# 1 模型功能介绍和技术说明文档
## 功能介绍：
*    应用场景：中石化中石油，
*    需求来源：
*    数据来源和数量：实际场景采集2w+，行人库筛选2w负样本，训练共37650，测试共5640

## 技术说明:
*    网络结构（GoogleNet+BN），数据增强（random crop + flip）

# 2 模型稳定性
* caffemodel: model/oil_dress.caffemodel
* prototxt: model/oil_dress.prototxt



# 3 前后处理逻辑
* classify_model_infer.py:
    (1) convert_image(image): 


# 4 模型转换
* 校准集：data/trt_data, 对应生成量化pb数据路径为：data/trt_data_pb
* 配置文件:
    (1) 模型配置文件: oil_dress.json
    
    (2) 引擎配置文件: engine_trt_int8_bs4.json

* 模型转换执行: Usage: ezm-gen <ENGINE> <MODEL_CONF_JSON> <OUTPUT_FILE> [ENGINE_CONF_JSON]
    (1) 转FP32: ezm-gen TRTEngine model/oil_dress.json model/oil_dress_FP32_4.ezm model/engine_trt_fp32_bs4.json
    
    (2) 转INT8:  ezm-gen TRTEngine model/oil_dress.json model/oil_dress_INT8_4.ezm model/engine_trt_int8_bs4.json

* 测试用例和测试结果




* 1 [HelloShell](https://github.com/AllenMao/Demo/tree/master/learningShell)

* 2 [Makefile](https://github.com/AllenMao/Demo/tree/master/learningShell/makefile)
** + 22222
+ aaaafdfad

* + 111111
+ fdsafad

 + 3333333
