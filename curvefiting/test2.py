# e的b/x次方拟合

#encoding=utf-8  
import numpy as np
import matplotlib.pyplot as plt
from scipy.optimize import curve_fit
 
#自定义函数 e指数形式
def func(x, a, b):
    return a*np.exp(b/x)
 
#定义x、y散点坐标
lon=[-169.8046,-163.4765,-157.5,-156.6210,-120.5859,-85.9570,-67.3242,-37.4414,-2.8125,58.7109,79.1015,96.3281,113.6266,113.8753,121.4648,165.4101]
lat=[24.0464,22.7559,23.2413,7.7109,5.4410,18.9790,29.5352,28.4590,35.0299,20.1384,15.7922,16.4676,21.8003,22.8711,25.1651,27.6835]

x=np.array(lon)
y=np.array(lat)

 
#非线性最小二乘法拟合
popt, pcov = curve_fit(func, x, y)
#获取popt里面是拟合系数
a = popt[0] 
b = popt[1]
yvals = func(x,a,b) #拟合y值
print ('系数a:', a)
print ('系数b:', b)
 
#绘图
plot1 = plt.plot(x, y, 's',label='original values')
plot2 = plt.plot(x, yvals, 'r',label='polyfit values')
plt.xlabel('x')
plt.ylabel('y')
plt.legend(loc=4) #指定legend的位置右下角
plt.title('curve_fit')
plt.show()
plt.savefig('test2.png')