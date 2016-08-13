#coding=utf-8
import pymysql
import requests
from lxml import etree

import numpy as np
import matplotlib.pyplot as plt
from matplotlib import cm


print '------------init sqldatabase--------------'
# 连接mysql数据库
# user为数据库的名字，passwd为数据库的密码，一般把要把字符集定义为utf8，不然存入数据库容易遇到编码问题
conn = pymysql.connect(host = 'localhost', user = 'hadoop', passwd = 'hadoop', db = 'mysql', charset = 'utf8')#192.168.37.134
cur = conn.cursor()  # 获取操作游标
cur.execute('use douban')  # 使用douban这个数据库

sql = "SELECT count(*) FROM information_schema.tables WHERE table_schema = 'douban' AND table_name = 'douban';"
#"select * from user_all_tables where table_name='douban';"
cur.execute(sql)
rows = cur.fetchall()
if(rows[0][0] == 1):
  print 'the table exists!!!'
else:
  sql = "CREATE TABLE `douban` ( \
    `id` int(11) NOT NULL AUTO_INCREMENT, \
    `类别` varchar(255) DEFAULT NULL, \
    `数量` varchar(255) DEFAULT NULL, \
    PRIMARY KEY (`id`) \
  ) ENGINE=InnoDB DEFAULT CHARSET=utf8 \
  AUTO_INCREMENT=1;"
  cur.execute(sql)
  #conn.commit()

print '----------------spider part----------------------'
douban = {}

def get_page(i):
  url = 'https://movie.douban.com/top250?start={}&filter='.format(i)
  html = requests.get(url).content.decode('utf-8')    # 使用request库获取网页内容
  selector = etree.HTML(html)    # 使用lxml库提取内容
  '''
  通过观察页面就能发现内容在<div class="info">下的一部分
  '''
  content = selector.xpath('//div[@class="info"]/div[@class="bd"]/p/text()')

  #print('a: ' + content[1].encode('utf-8') + 'aa')

  #print('b: ' + str(content[1].replace(u'\xa0','z')).strip().replace(' ', ''))

  for i in content[1::4]:
    i = i.replace(u'\xa0','')
    i = i.encode('utf-8')# code is error,need convert to utf8

    #print('b: ' + str(i).strip().replace('\n\r', ''))
    #print('z:' + str(i).strip().replace(' ', ''))
    i = str(i).split('/')
    i = i[len(i) - 1]
    #i = i.strip().replace(u'\xc2', '') #drop the error ascii
    #i = i.strip().replace(u'\xa0', '') #drop the error ascii

    key = i.strip().replace('\n', '').split(' ') # 这里的strip和replace的使用目的是去除空格和空行之类
    #print 'c'
    print(key)
    for j in key:#把250部电影的分类汇总数量
      j = j.strip().replace(' ','')
      print j
      if j not in douban.keys():
        douban[j] = 1
      else:
        douban[j] += 1

print '----------------save data to mysql----------------------------'

def save_mysql(douban):
  #print(douban)  # douban在主函数中定义的字典
  for key in douban:
    print(key)
    print(douban[key])
    if key != '':
      try:
        sql = 'insert douban(类别, 数量) value(' + "\'" + key + "\'," + "\'" + str(douban[key]) + "\'" + ');'
        cur.execute(sql)
        conn.commit()
      except:
        print('插入失败')
      conn.rollback()

'---------------visual data-------------------'
def pylot_show():
  sql = 'select * from douban;'  
  cur.execute(sql)
  rows = cur.fetchall()   # 把表中所有字段读取出来
  count = []   # 每个分类的数量
  category = []  # 分类

  for row in rows:
    count.append(int(row[2]))   
    category.append(row[1])
    y_pos = np.arange(len(category))    # 定义y轴坐标数

    #color = cm.jet(np.array(2)/max(count))

    plt.barh(y_pos, count, color='y', align='center', alpha=0.4)  # alpha图表的填充不透明度(0~1)之间
    plt.yticks(y_pos, category)  # 在y轴上做分类名的标记
    plt.grid(axis = 'x')

  for count, y_pos in zip(count, y_pos):
    # 分类个数在图中显示的位置，就是那些数字在柱状图尾部显示的数字
    plt.text(count+3, y_pos, count,  horizontalalignment='center', verticalalignment='center', weight='bold')  
    plt.ylim(+28.0, -2.0) # 可视化范围，相当于规定y轴范围
    plt.title('douban_top250')   # 图表的标题   fontproperties='simhei'
    plt.ylabel('movie category')     # 图表y轴的标记
    plt.subplots_adjust(bottom = 0.15) 
    plt.xlabel('count')  # 图表x轴的标记
    #plt.savefig('douban.png')   # 保存图片
  plt.show()



def main():

  #for i in xrange(0,226,25):
  #  get_page(i)
  
  #save_mysql(douban) #have added

  pylot_show()

  cur.close()
  conn.close()

if __name__ == "__main__":
  main();