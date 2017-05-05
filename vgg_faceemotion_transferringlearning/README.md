### 前言
> 本实验将分享如何采用迁移学习的方法，在少量的数据上，完成人脸表情识别。所述的人脸表情识别，实际上是人脸表情分类问题。

### 1 准备数据
* [dataset download](https://drive.google.com/open?id=0B3ANX1iL124qbmxOc2cyQzhvUFE)：包含7种表情，其中10333张图像用作训练集，4424张图像用作测试集。

* 执行data目录下：create_lmdb.sh

### 2 迁移学习
* 迁移学习：获取预先训练好的神经网络模型的权值分享到一个相似的任务上。一方面节省了训练的时间；另一方面我们可以在少量数据集上进行训练。

* 参数转移：通过transfer learning可以将已经学到的权值，分享给新模型从而加快并优化模型的学习不用像之前那样重新开始。
> net_surgery.py:
> func:实现将已训练好的模型部分权值，复制到当前任务的模型上，并生成对应的caffemodel文件。
> Trained model: VGG16
> New model: 只将fc8的节点数改为7

* 实验方案：

> 实验中采用了三种方案，从左到右依次是：训练所有层、只训练全连接层、只训练全连接层和最后一个卷积组。
![scheme.jpg](https://github.com/AllenMao/Demo/blob/master/vgg_faceemotion_transferringlearning/results/scheme.jpg?raw=true)


### 3 训练细节

> 学习率固定为： 0.00001；迭代：1w次；最后只需要执行solve.py

### 4 Results

* 1: 训练所有层

![201705031514.png](https://github.com/AllenMao/Demo/blob/master/vgg_faceemotion_transferringlearning/results/201705031514.png?raw=true)

* 2: 只训练全连接层

![201705031729.png](https://github.com/AllenMao/Demo/blob/master/vgg_faceemotion_transferringlearning/results/201705031729.png?raw=true)

* 3: freeze conv1-4

![201705031941.png](https://github.com/AllenMao/Demo/blob/master/vgg_faceemotion_transferringlearning/results/201705031941.png?raw=true)

### 参考
[1] Simonyan K, Zisserman A. Very Deep Convolutional Networks for Large-Scale Image Recognition. 2014.

[2] http://cs231n.github.io/

