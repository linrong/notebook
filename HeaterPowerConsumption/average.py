import pandas as pd
import os
import datetime

path1=os.getcwd()+'\\current_pid1.csv'
path2=os.getcwd()+'\\current_pid2.csv'
path3=os.getcwd()+'\\current_pid3.csv'
path4=os.getcwd()+'\\控制器设备信息反馈-2018-09-19.csv'

path5=os.getcwd()+'\\1.csv'
path6=os.getcwd()+'\\2.csv'
path7=os.getcwd()+'\\3.csv'

file1=open(path1,encoding='utf-8')
file2=open(path2,encoding='utf-8')
file3=open(path3,encoding='utf-8')
file4=open(path4,encoding='utf-8')

df1=pd.read_csv(file1, header=None)
df2=pd.read_csv(file2, header=None)
df3=pd.read_csv(file3, header=None)
df4=pd.read_csv(file4)
# 前五行数据
# headdata=df4.head(5)
# print(headdata)
"""
count:行数
df:数据
path:存储位置
"""
def getaverage(count,df,path):
    avelist=[]
    datelist=[]
    wlist=[]
    for i in range(count):
        rowdata=df.ix[i,:]
        n=0
        nums=0
        # 求此行的平均值
        for m in range(99):
            number=0
            try:
                number=int(rowdata[m])
            except Exception:
                pass
            if number>1400:
                nums+=number
                n+=1
        ave=nums/(n if n>0 else 1)
        # 处理日期
        try:
            datestr=rowdata[100].lstrip('[').rstrip(']') 
        except Exception:
            continue
        date=datetime.datetime.strptime(datestr,'%Y-%m-%d %H:%M:%S.%f')+datetime.timedelta(hours=12)
        # 获得消耗
        w=geteneryconsumption(date,ave,len(df4),df4)
        # 获得此页的消耗
        datelist.append(date)
        avelist.append(ave)
        wlist.append(w)

    dit={'日期':datelist,'平均值':avelist,'消耗':wlist}
    savedf=pd.DataFrame(dit)
    #生成csv文件
    savedf.to_csv(path,columns=['日期','平均值','消耗'],index=True,sep=',',encoding="utf_8_sig")
def geteneryconsumption(datadate,ave,count,df):
    global start
    for i in range(start,count):
        rowdata=df.ix[i,['本地保存时间','载荷8电压(mV)']]
        # 处理日期
        date=datetime.datetime.strptime(rowdata[0],'%Y-%m-%d %H:%M:%S')
        w=0
        print('{id}:{date}'.format(id=i,date=date))
        print (date>datadate)
        if date > datadate:
            start=i
            w=(ave/1000)*(int(rowdata[1])/1000)*24*0.1
            return w
    return 0
start=0
getaverage(len(df1),df1,path5)
start=0
getaverage(len(df2),df2,path6)
start=0
getaverage(len(df3),df3,path7)


