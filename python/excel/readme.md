#### 使用pyqt5
```bash
pip install pyqt5-tools -i https://mirrors.aliyun.com/pypi/simple/
这个会安装pyqt5
命令行输入designer(git bash/powershell),启动pyqt的UI设计,保存.ui到文件夹中
pyuic5 mainwindown.ui -o mainwindown.py
通过ui文件生成py文件,然后在主主程序中进行引用
```
> [使用 vscode 从零开始学习 PyQt5](https://www.jianshu.com/p/c37c5b1c9a5e)

> [[PyQt5]文件对话框QFileDialog的使用](https://blog.csdn.net/qq_36556893/article/details/90714070)

#### 打包
```bash
pip install pyinstaller -i https://mirrors.aliyun.com/pypi/simple/
pyinstaller -F -w mergetocsv.py

# 报错
24240 INFO: Building PYZ (ZlibArchive) D:\github\notebook\python\csv\build\mergetocsv\PYZ-00.pyz
Traceback (most recent call last):
  File "c:\users\lazyh\appdata\local\programs\python\python38-32\Lib\runpy.py", line 192, in _run_module_as_main
    return _run_code(code, main_globals, None,
  File "c:\users\lazyh\appdata\local\programs\python\python38-32\Lib\runpy.py", line 85, in _run_code
    exec(code, run_globals)
  File "E:\virtualenv\script\Scripts\pyinstaller.exe\__main__.py", line 7, in <module>
  File "e:\virtualenv\script\lib\site-packages\PyInstaller\__main__.py", line 111, in run
    run_build(pyi_config, spec_file, **vars(args))
  File "e:\virtualenv\script\lib\site-packages\PyInstaller\__main__.py", line 63, in run_build
    PyInstaller.building.build_main.main(pyi_config, spec_file, **kwargs)
  File "e:\virtualenv\script\lib\site-packages\PyInstaller\building\build_main.py", line 844, in main
    build(specfile, kw.get('distpath'), kw.get('workpath'), kw.get('clean_build'))
  File "e:\virtualenv\script\lib\site-packages\PyInstaller\building\build_main.py", line 791, in build
    exec(code, spec_namespace)
  File "D:\github\notebook\python\csv\mergetocsv.spec", line 18, in <module>
    pyz = PYZ(a.pure, a.zipped_data,
  File "e:\virtualenv\script\lib\site-packages\PyInstaller\building\api.py", line 98, in __init__
    self.__postinit__()
  File "e:\virtualenv\script\lib\site-packages\PyInstaller\building\datastruct.py", line 158, in __postinit__
    self.assemble()
  File "e:\virtualenv\script\lib\site-packages\PyInstaller\building\api.py", line 128, in assemble
    self.code_dict = {
  File "e:\virtualenv\script\lib\site-packages\PyInstaller\building\api.py", line 129, in <dictcomp>
    key: strip_paths_in_code(code)
  File "e:\virtualenv\script\lib\site-packages\PyInstaller\building\utils.py", line 652, in strip_paths_in_code
    consts = tuple(
  File "e:\virtualenv\script\lib\site-packages\PyInstaller\building\utils.py", line 653, in <genexpr>
    strip_paths_in_code(const_co, new_filename)
  File "e:\virtualenv\script\lib\site-packages\PyInstaller\building\utils.py", line 660, in strip_paths_in_code
    return code_func(co.co_argcount, co.co_kwonlyargcount, co.co_nlocals, co.co_stacksize,
TypeError: an integer is required (got type bytes)
(script)

# 解决
报错前的pyinstaller是用 pip install pyinstaller 来安装的，改成用 pip install https://github.com/pyinstaller/pyinstaller/archive/develop.tar.gz 再安装一次
https://blog.csdn.net/chen_soldier/article/details/102667201
```