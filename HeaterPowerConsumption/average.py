import pandas as pd
import os

path1=os.getcwd()+'\\current_pid1.csv'
path2=os.getcwd()+'\\current_pid2.csv'
path3=os.getcwd()+'\\current_pid3.csv'

file1=open(path1,encoding='utf-8')
file2=open(path2,encoding='utf-8')
file3=open(path3,encoding='utf-8')

data1=pd.read_csv(file1, header=None)
data2=pd.read_csv(file2, header=None)
data3=pd.read_csv(file3, header=None)
# 前五行数据
# headdata=data1.head(5)
# print(headdata)


rowdata=data1.ix[0,:]
print(rowdata)