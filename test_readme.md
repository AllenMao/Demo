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
    
    (1) Usage: python classify_model_infer.py <MODEL_NAME> <EZM_MODEL> <OUTPUT_NAME> <MODE>
        
        Parameters: 
            - MODEL_NAME: "model_name" defined in MODEL_CONF_JSON
            - EZM_MODEL:  generated MODEL (e.g. FP32.ezm, INT8.ezm)
            - OUTPUT_NAME: output layer name of model
            - MODE: 'dev' for evaluation; 'test' only get label; 'save_pb' for converting images to pb file
    
    (2) 必要函数介绍：
    
        convert_image(image): 图像预处理函数，包括 resize，减均值
    
    

# 4 模型转换
## 校准集:
*    部分训练集: data/trt_data
*    量化所需pb数据：data/trt_data_pb
## 配置文件:
*    模型配置文件: model/oil_dress.json
*    引擎配置文件: model/engine_trt_int8_bs4.json

## 模型转换执行: 
     Usage: ezm-gen <ENGINE> <MODEL_CONF_JSON> <OUTPUT_FILE> [ENGINE_CONF_JSON]
*    转FP32:

         (1) 配置好模型配置文件 (model/oil_dress.json) 和引擎配置文件 (model/engine_trt_fp32_bs4.json)
         
         (2) 执行shell: ezm-gen TRTEngine model/oil_dress.json model/oil_dress_FP32_4.ezm model/engine_trt_fp32_bs4.json

*    转INT8:

         (1) 准备好训练集（随机挑选num_cls * 1k即可)，生成pb文件，执行python classify_model_infer.py 'oil_dress' 'model/oil_dress_FP32_4.ezm' 'prob3' 'save_pb'，保存在data下生成trt_data_pb
         
         (2) 配置好模型配置文件 (model/oil_dress.json) 和引擎配置文件 (model/engine_trt_int8_bs4.json)
         
         (3) 执行shell: ezm-gen TRTEngine model/oil_dress.json model/oil_dress_INT8_4.ezm model/engine_trt_int8_bs4.json

## 测试用例和测试结果
*    test_samples: data/test_samples，提供少许图像
*    test_samples_resules: data/test_samples_resules，测试样例int8 trt输出结果。用于集成组转换后的模型输出diff算法组提供的结果。
*    测试性能的测试报告: 提供GPU单卡batch_size=8的显存和耗时，仅供参考；例如（GTX1080ti. batch-size=8情况下的显存和耗时）

# 5 模型转换一致性
*    一致性验证结果: 原始模型和转换后的模型的曲线（验证模型是否转换成功）[pdf](https://gitlab.deepglint.com)

         python classify_model_infer.py 'oil_dress' 'model/oil_dress_INT8_4.ezm' 'prob3' 'dev'

# 6 阈值设置参考
*    提供曲线对应的点的信息列表（p，r，threshold）[txt](https://gitlab.deepglint.com)
