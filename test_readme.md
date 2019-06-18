GENERAL:
  CUDA_VISIBLE_DEVICES:  '0,1'


DATASET:
  NAME: 'FACE'
  SUB_DIR: ''
  TRAIN_SETS: (('sndgface', 'sndgcartrain'), )
  # TEST_SETS:  (('sndgface', 'dgtesthaveann'), )
  TEST_SETS:  (('sndgface', 'sntest'), )
  NUM_CLASSES: 1
  IMAGE_SIZE: (360, 640)
  TRAIN_BATCH_SIZE: 128
  NUM_WORKERS: 32

TRAIN:
  MAX_ITER: 20000
# evaluate every eval_iter
  EVAL_ITER: 1000
# save models every
  SAVE_ITER: 1000
# log loss every
  LOG_LOSS_ITER: 100
  WARMUP_EPOCH: 2
  LR_SCHEDULER:
    STEPS: (7000, 14000, 18000) 
  OPTIMIZER:
    LR: 0.1

MODEL:
  SSD_TYPE: 'SSD_SN'
  BASE: 'res10_t'
  PRETRAIN_MODEL: ' '
  STEPS: [4, 8, 16]
  MIN_SIZES: [[10, 14], [21.2,32,48],[70,105]]
  MAX_SIZES: []
  FLIP: False
  ASPECT_RATIOS: [[], [], []]
  SSD:
    EXTRA_CONFIG: ('p2_96', 'p2_96', 'p2_96', 'p3_96', 'p3_96', 'p3_96')

import torch
import torch.nn as nn

def conv3x3(in_channels, out_channels, stride=1):
    return nn.Conv2d(in_channels, out_channels, kernel_size=3,
                     stride=stride, padding=1, bias=False)
def conv1x1(in_channels, out_channels, stride=1):
    return nn.Conv2d(in_channels, out_channels, kernel_size=1,
                     stride=stride, padding=0, bias=False)


class ResidualBlock_small(nn.Module):
    def __init__(self, in_channels, out_channels, stride=1,conv_2=False):
        super(ResidualBlock_small, self).__init__()
        self.conv1 = conv3x3(in_channels, out_channels, stride=stride)
        self.bn1 = nn.BatchNorm2d(out_channels)
        self.relu = nn.ReLU(inplace=True)
        self.conv2 = conv3x3(out_channels, out_channels)
        self.bn2 = nn.BatchNorm2d(out_channels)
        self.conv_2 = conv_2
        if conv_2 == True:
            self.conv1_1 = conv1x1(in_channels, out_channels, stride=stride)
            self.bn1_2 = nn.BatchNorm2d(out_channels)


    def forward(self, x):
        residual = x
        out = self.conv1(x)
        out = self.bn1(out)
        out = self.relu(out)
        out = self.conv2(out)
        out = self.bn2(out)
        if self.conv_2:
            residual = self.conv1_1(x)
            residual = self.bn1_2(residual)
        out += residual
        out = self.relu(out)
        return out


# def res10():

#     layers = []
#     in_channels = 3
#     all_channels = 12
#     conv2d = nn.Conv2d(in_channels, all_channels, kernel_size=3, stride=2, padding=1)
#     layers += [conv2d, nn.BatchNorm2d(all_channels), nn.ReLU(inplace=True)]
#     layers += [nn.MaxPool2d(kernel_size=3, stride=2, padding=0,ceil_mode=True)]
#     layers += [ResidualBlock_small(all_channels,all_channels,1,True)]
#     layers += [ResidualBlock_small(all_channels,all_channels,2,True)]
#     layers += [ResidualBlock_small(all_channels,all_channels,2,True)]
#     return layers
def res10():

    layers = []
    in_channels = 3
    all_channels = 12
    conv2d = nn.Conv2d(in_channels, 19, kernel_size=3, stride=2, padding=1)
    layers += [conv2d, nn.BatchNorm2d(19), nn.ReLU(inplace=True)]
    layers += [nn.MaxPool2d(kernel_size=3, stride=2, padding=0,ceil_mode=True)]
    layers += [ResidualBlock_small(19,32,1,True)]
    layers += [ResidualBlock_small(32,64,2,True)]
    layers += [ResidualBlock_small(64,96,2,True)]
    return layers

# def res10_t():

#     layers = []
#     in_channels = 3
#     conv2d = nn.Conv2d(in_channels, 16, kernel_size=3, stride=2, padding=1)
#     layers += [conv2d, nn.BatchNorm2d(16), nn.ReLU(inplace=True)]
#     layers += [nn.MaxPool2d(kernel_size=3, stride=2, padding=0,ceil_mode=True)]
#     layers += [ResidualBlock_small(16,32,1,True)]
#     layers += [ResidualBlock_small(32,64,2,True)]
#     layers += [ResidualBlock_small(64,96,2,True)]
#     return layers


def res10_t():

    layers = []
    in_channels = 3
    conv2d = nn.Conv2d(in_channels, 19, kernel_size=3, stride=2, padding=1)
    layers += [conv2d, nn.BatchNorm2d(19), nn.ReLU(inplace=True)]
    layers += [nn.MaxPool2d(kernel_size=3, stride=2, padding=0,ceil_mode=True)]
    layers += [ResidualBlock_small(19,32,1,True)]
    layers += [ResidualBlock_small(32,64,2,True)]
    layers += [ResidualBlock_small(64,96,2,True)]
    return layers
    
    

import torch
import torch.nn as nn
import torch.nn.functional as F
from lib.layers import *


class SSD_SN(nn.Module):
    """Single Shot Multibox Architecture
    The network is composed of a base VGG network followed by the
    added multibox conv layers.  Each multibox layer branches into
        1) conv2d for class conf scores
        2) conv2d for localization predictions
        3) associated priorbox layer to produce default bounding
           boxes specific to the layer's feature map size.
    See: https://arxiv.org/pdf/1512.02325.pdf for more details.

    Args:
        phase: (string) Can be "test" or "train"
        size: input image size
        base: VGG16 layers for input, size of either 300 or 500
        extras: extra layers that feed to multibox loc and conf layers
        head: "multibox head" consists of loc and conf conv layers
    """

    def __init__(self, phase, cfg, base):
        super(SSD_SN, self).__init__()
        if phase != "train" and phase != "eval" and phase != "mimic" and phase != "caffe":
            raise Exception("ERROR: Input phase: {} not recognized".format(phase))
        self.phase = phase
        self.num_classes = cfg.MODEL.NUM_CLASSES
        self.cfg = cfg
        self.priors = None
        self.image_size = cfg.MODEL.IMAGE_SIZE

        # SSD network
        self.base = nn.ModuleList(base)
        # Layer learns to scale the l2 normalized features from conv4_3
        #self.L2Norm = L2Norm(512, 20)  # TODO automate this
        
        if(cfg.MODEL.SSD.EXTRA_CONFIG_T[0]!=' '):
            extras = add_extras(cfg.MODEL.SSD.EXTRA_CONFIG_T, base)

            head = multibox(base,cfg.MODEL.SSD.EXTRA_CONFIG_T, cfg.MODEL.NUM_PRIOR, cfg.MODEL.NUM_CLASSES)
        else:
            extras = add_extras(cfg.MODEL.SSD.EXTRA_CONFIG, base)

            head = multibox(base,cfg.MODEL.SSD.EXTRA_CONFIG, cfg.MODEL.NUM_PRIOR, cfg.MODEL.NUM_CLASSES)
        self.extras = nn.ModuleList(extras)
        self.loc = nn.ModuleList(head[0])
        self.conf = nn.ModuleList(head[1])

        self.softmax = nn.Softmax(dim=-1)
        # self.matching = None
        self.criterion = None
        # if self.phase == 'eval':  # TODO add to config
        #     self.detect = Detect(self.num_classes, 0, 200, 0.01, 0.45)

    def forward(self, x, phase='train', match_result=None, tb_writer=None):
        sources = list()
        loc = list()
        conf = list()

        phase = phase

        # dstf1 = open("/home/users/xupengfei/pytorch/ssd.pytorch/data.txt",'w')
        # dstf1.write(x)
        # print("x",x)
        # apply vgg up to conv4_3 relu
        for k in range(7):  # TODO make it configurable
            x = self.base[k](x)
            if k > 3:
                sources.append(x)


        # s = self.L2Norm(x)  # can replace batchnorm    nn.BatchNorm2d(x)#
        #sources.append(s)

        # apply vgg up to fc7
        # for k in range(7, len(self.base)):
        #     x = self.base[k](x)
        # sources.append(x)

        # apply extra layers and cache source layer outputs
        # for k, v in enumerate(self.extras):
        #     sources[k] = F.relu(v(sources[k]), inplace=True)
        headnum = len(self.extras)/2 #TODO head config 

        for k, v in enumerate(self.extras):
            idx = int(k//headnum)
            sources[idx] = v(sources[idx])
        # apply multibox head to source layers
        for (source, l, c) in zip(sources, self.loc, self.conf):
            loc.append(l(source).permute(0, 2, 3, 1).contiguous())
            conf.append(c(source).permute(0, 2, 3, 1).contiguous())

        loc = torch.cat([o.view(o.size(0), -1) for o in loc], 1)
        conf = torch.cat([o.view(o.size(0), -1) for o in conf], 1)
        loc = loc.view(loc.size(0), -1, 4)
        conf = conf.view(conf.size(0), -1, self.num_classes)
        
        if phase == 'eval':
            output = loc, self.softmax(conf)
        elif phase == 'train':

            output = self.criterion((loc, conf), match_result, tb_writer) \
                if self.criterion is not None else None
            # output = loc, conf
        elif phase == 'caffe':
            output = (loc, conf)
        else:
            output = sources

        return output


def add_extras(cfg_extra, base, batch_norm=False):
    # Extra layers added to VGG for feature scaling
    layers = []
    in_channels_p2 = 0
    in_channels_p3 = 0
    for idx, v in enumerate(cfg_extra):
        if 'p2' in v:
            blockidx = -3 
            if in_channels_p2 == 0:
                in_channels_p2 = base[blockidx].conv2.out_channels  # TODO make this configurable
            layers += [nn.Conv2d(in_channels_p2, int(v[3:]),kernel_size=1, stride=1)]
            layers += [nn.ReLU(inplace=True)]
            in_channels_p2 = int(v[3:])
        elif 'p3' in v:
            blockidx = -2 
            if in_channels_p3 == 0:
                in_channels_p3 = base[blockidx].conv2.out_channels  # TODO make this configurable
            layers += [nn.Conv2d(in_channels_p3, int(v[3:]),kernel_size=1, stride=1)]
            layers += [nn.ReLU(inplace=True)]
            in_channels_p3 = int(v[3:])
    return layers


def multibox(base, cfg,num_priors, num_classes):
    loc_layers = []
    conf_layers = []

    for k, v in enumerate(cfg):  
        if 'p2' in v:
            in_channel_p2 = int(v[3:])
        elif 'p3' in v:
            in_channel_p3 = int(v[3:])
    loc_layers += [nn.Conv2d(in_channel_p2, num_priors[0]
                             * 4, kernel_size=3, padding=1)]
    conf_layers += [nn.Conv2d(in_channel_p2, num_priors[0]
                              * num_classes, kernel_size=3, padding=1)]
    loc_layers += [nn.Conv2d(in_channel_p3, num_priors[1]
                             * 4, kernel_size=3, padding=1)]
    conf_layers += [nn.Conv2d(in_channel_p3, num_priors[1]
                              * num_classes, kernel_size=3, padding=1)]
    loc_layers += [nn.Conv2d(base[-1].conv2.out_channels, num_priors[-1]
                             * 4, kernel_size=3, padding=1)]
    conf_layers += [nn.Conv2d(base[-1].conv2.out_channels, num_priors[-1]
                              * num_classes, kernel_size=3, padding=1)]
    return loc_layers, conf_layers

#
# extras_config = {
#     'ssd': [256, 'S', 512, 128, 'S', 256, 128, 256, 128, 256],
# }


if __name__ == '__main__':
    from lib.utils.config import cfg
    from lib.models import model_factory

    net, priors, layer_dims = model_factory(phase='train', cfg=cfg)
    print(net)
    # print(priors)
    # print(layer_dims)

    # input_names = ['data']
    # net = net.cuda()
    # dummy_input = Variable(torch.randn(1, 3, 300, 300)).cuda()
    # torch.onnx.export(net, dummy_input, "./cache/alexnet.onnx", export_params=False, verbose=True, )
