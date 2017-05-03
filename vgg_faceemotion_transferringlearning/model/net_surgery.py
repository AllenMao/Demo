#!/usr/bin/python2

import sys,os;
sys.path.append("/home/usrname/usr/caffe/python")

import caffe


SRC_PROTOTXT_FILENAME = "./models/vgg16_deploy.prototxt"
SRC_MODEL_FILENAME = "./models/vgg16.caffemodel"

DST_PROTOTXT_FILENAME = "./models/vgg16_emo_deploy.prototxt"
DST_MODEL_FILENAME = "./models/vgg16_emo.caffemodel"


def generate_model():

    caffe.set_mode_cpu()

    print "Loading src model..."
    src = caffe.Net(SRC_PROTOTXT_FILENAME, SRC_MODEL_FILENAME, caffe.TEST)

    print "Loading dst prototxt..."
    dst = caffe.Net(DST_PROTOTXT_FILENAME, caffe.TEST)

    print "Transplanting parameters..."
    transplant(dst, src)

    print "Saving new model to %s " % DST_MODEL_FILENAME
    dst.save(DST_MODEL_FILENAME)


def transplant(new_net, net, suffix=''):
    # from fcn.berkeleyvision.org
    for p in net.params:
        p_new = p + suffix
        if p_new not in new_net.params:
            print 'dropping', p
            continue
        for i in range(len(net.params[p])):
            if i > (len(new_net.params[p_new]) - 1):
                print 'dropping', p, i
                break
            if net.params[p][i].data.shape != new_net.params[p_new][i].data.shape:
                print 'coercing', p, i, 'from', net.params[p][i].data.shape, 'to', new_net.params[p_new][i].data.shape
            else:
                print 'copying', p, ' -> ', p_new, i
            new_net.params[p_new][i].data.flat = net.params[p][i].data.flat


if __name__ == '__main__':
    generate_model()
