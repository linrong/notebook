# curvefiting

#### 项目介绍
曲线拟合


#### vitualenv和windows10
* 安装numpy
```
pip install numpy
```
* 安装scipy
```
pip install scipy
```
* 安装matplotlib
```
pip install matplotlib
```
* wrong
```
Command "python setup.py egg_info" failed with error code 1 in C:\Users\lazyh\AppData\Local\Temp\pip-install-7x9on70_\Matplotlib\
```

* solution:
```
利用wheel安装
pip install wheel

访问:http://www.lfd.uci.edu/~gohlke/pythonlibs/下载对应Python版本的编译包
```
>如下图

![matplotlib](https://images.gitee.com/uploads/images/2018/0919/171948_6b20262e_1594515.png "matplotlib")
![版本说明](https://images.gitee.com/uploads/images/2018/0919/172036_1c15f3a5_1594515.png "版本")

```
版本说明：
matplotlib-1.5.3-cp35-cp35m-win32.whl (md5)
    1.5.3 : plt版本
    cp35  : python 版本
    win32 : Windows 32位系统
    whl   : wheel 文件 

# 文件目录下安装
pip install matplotlib-3.0.0-cp36-cp36m-win32.whl

# 安装成功
```

#### 使用VSCode开发
1. 下载安装VSCode
2. VSCode安装插件Python，Python for VSCode
3. VSCode中打开python项目的文件夹，会自动生成launch.json和settings.json文件
4. 设置虚拟环境目录,打开settings.json文件，在WORKSPACE SETTINGS中添加目录，如下
![输入图片说明](https://images.gitee.com/uploads/images/2018/0920/105451_e0f1d89a_1594515.png "python.venvPath")
5. 按下快捷键Ctrl+Shift+P，选择python的路径，如下
![输入图片说明](https://images.gitee.com/uploads/images/2018/0920/105817_93b9066f_1594515.png "选择解析器.png")
![输入图片说明](https://images.gitee.com/uploads/images/2018/0920/105825_70c08a05_1594515.png "python路径.png")
6. 选择合适的python进行开发

#### VSCode的终端
##### powershell
1. 使用workon或者在Scripts下运行activate.bat
>结果：运行失败，不能进入虚拟环境,查阅据说在power shell下应该使用ps脚本，而virtualenv自带的有Scripts\activate.ps1
2. 使用activate.ps1
>结果：运行错误，错误提示：请参阅 https:/go.microsoft.com/fwlink/?LinkID=135170 中的 about_Execution_Policies

>参阅结合其他资料大概认为是：为了安全起见，power shell不能运行脚本，运行以下命令即可开启脚本运行权限

>命令1：Set-ExecutionPolicy -ExecutionPolicy UNRESTRICTED 

>或者命令2：Set-ExecutionPolicy -Scope CurrentUser

>请为以下参数提供值：ExecutionPolicy:remotesigned

>Scripts目录下运行./activate
##### cmd
1. 修改终端使用cmd,在settings.json文件，USER SETTINGS中设置如下：
```
{
    "terminal.integrated.shell.windows": "C:\\Windows\\System32\\cmd.exe"
}

而默认的终端设置为：
 "terminal.integrated.shell.windows": "C:\\WINDOWS\\System32\\WindowsPowerShell\\v1.0\\powershell.exe"
```
>这样子通过Ctrl+`打开的终端都是cmd的,而不是powershell
2. 设置打开终端默认进入虚拟环境
在settings.json中，WORKSPACE SETTINGS中新加设置如下：
```
{
    "terminal.integrated.shellArgs.windows": ["/k", "E:\\virtualenv\\python3\\Scripts\\activate"]  
}
```
>通过此设置可以达到打开终端即运行进入虚拟环境