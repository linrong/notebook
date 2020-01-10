'''
pip install pandas
pip install openpyxl
pip install pyqt5-tools -i https://mirrors.aliyun.com/pypi/simple/
'''
import pandas as pd
import numpy as np
import sys
import os
import time
import threading

from mainwindown import Ui_MainWindow
from PyQt5.QtWidgets import *
from PyQt5.QtCore import *

def getdatabyexcel(excelpath):
    try:
        # 循环处理excel文件
        allfilelist = os.listdir(excelpath)
        excelfilelist = []
        for file in allfilelist:
            filepath = os.path.join(excelpath, file)
            # 判断是否文件
            if os.path.isfile(filepath):
                if file.endswith('.xlsx') and not file.startswith('~$'):
                    excelfilelist.append(file)

        if len(excelfilelist) > 1:
            example.signal.emit("开始处理excel...")
            # 获取一份单独的excel
            excelname = excelfilelist[0]
            path = os.path.join(excelpath, excelname)
            # 这个会直接默认读取到这个Excel的第一个表单
            df1 = pd.read_excel(path, sheet_name='Sheet1')
            # 获取行数
            row = df1.shape[0] # 行数
            col = df1.shape[1] # 列数

            row_i = -1 # 物料型号行下标
            for i in range(row):
                if '物料型号' in df1.loc[i].values:
                    row_i = i
                    break
            
            col_type = -1 # 物料型号列下标
            col_num = -1 # 数量列下标
            col_type = int(np.argwhere(df1.loc[row_i].values=='物料型号'))
            col_num = int(np.argwhere(df1.loc[row_i].values=='数量'))

            for index, file in enumerate(excelfilelist):
                filepath = os.path.join(excelpath, file)
                # 判断是否文件
                if os.path.isfile(filepath):
                    if file.endswith('.xlsx') and not file.startswith('~$') and file != excelname:
                        df = pd.read_excel(filepath, sheet_name='Sheet1')
                        # 获取行数
                        row_for = df.shape[0] # 行数
                        col_for = df.shape[1] # 列数

                        row_i_for = -1 # 物料型号行下标
                        for i in range(row_for):
                            if '物料型号' in df.loc[i].values:
                                row_i_for = i
                                break
                        
                        col_type_for = -1 # 物料型号列下标
                        col_num_for = -1 # 数量列下标
                        col_type_for = int(np.argwhere(df.loc[row_i_for].values=='物料型号'))
                        col_num_for = int(np.argwhere(df.loc[row_i_for].values=='数量'))
                        
                        # 循环对比两个excel中的物料型号
                        row_i_for+=1 # 从正式数据开始计算
                        while row_i_for < row_for:
                            # 先处理excel列表
                            # 获取物料型号
                            typename_for = df.loc[row_i_for][col_type_for]
                            # 获取物料数量
                            num_for = df.loc[row_i_for][col_num_for]

                            # 是否存在相同的物料型号
                            ishas = False
                            row_i_while = row_i + 1
                            while row_i_while < row:
                                if typename_for == df1.loc[row_i_while][col_type]:
                                    # 有相同的型号,数量相加
                                    # print(df1.loc[row_i_while][col_num])
                                    df1.loc[row_i_while][col_num] = df1.loc[row_i_while][col_num] + num_for
                                    ishas = True
                                    # print(df1.loc[row_i_while][col_num])
                                    # print('----------')
                                row_i_while+=1
                            # 没有相同的型号
                            if not ishas:
                                df1.loc[row] = df.loc[row_i_for]
                                # 编号的增加
                                df1.loc[row][0] = df1.loc[row - 1][0] + 1
                                # 重新统计行数
                                row = df1.shape[0]
                                # print(row)
                                # print(df1.loc[57][col_type])
                            row_i_for+=1 
                example.signal.emit('处理excel数量:' + str(index))

            # 进行排序,目前排序还是有问题

            row = df1.shape[0]
            list_data = []
            row_i_while = row_i + 1
            while row_i_while < row:
                list_data.append(df1.loc[row_i_while])
                row_i_while+=1

            # list_data_sort = sorted(list_data, key=(lambda x:x[1]))
            # 冒泡排序
            for i in range (0, len(list_data)):
                for j in range(0, len(list_data) - i - 1 ):
                    if list_data[j][1] > list_data[j + 1][1]:
                        (list_data[j],list_data[j+1])=(list_data[j+1],list_data[j])
            '''
            之前使用df1进行替换，因为对象的原因，替换df1中的行时，也把list_data中数据替换了，所以会导致数据错误
            现在提取df1中前面几行来和新排序好的数据进行生成一个新的pd
            '''
            df2 = df1[:row_i + 1]
            count = 0
            row_i_while = row_i + 1
            while row_i_while < row:
                df2.loc[row_i_while] = list_data[count]
                df2.loc[row_i_while][0] = count + 1
                row_i_while+=1
                count+=1
            example.signal.emit('排序完成...')
            df2.to_excel(os.path.join(os.getcwd(), '汇总物料' + '_' + str(int(time.time())) + '.xlsx'), sheet_name = 'Sheet1', index = False)
            example.signal.emit('处理保存地址:' + os.getcwd())
        elif len(excelfilelist) == 1:
            example.signal.emit('文件夹下只有一份excel文件,暂不处理')
        else:
            example.signal.emit('文件夹下没有excel文件,暂不处理')
    except Exception as e:
        example.signal.emit(e)

class Example(QWidget):
    signal = pyqtSignal(str)# 括号里填写信号传递的参数

class MainForm(QMainWindow, Ui_MainWindow):
    # 继承于自父类QtWidgets.QMainWindow,mainwindow中的Ui_MainWindow

    # parent = None代表此QWidget属于最上层的窗口,也就是MainWindows
    def __init__(self, parent=None):
        # 因为继承关系，要对父类初始化
        super(MainForm, self).__init__()
        self.setupUi(self)
        example.signal.connect(self.updatelabel)

    def getfolderpath(self):
        # 此方法和UI的绑定在ui文件中,在ui文件中把按钮的clicked信号和MainWindow.getfolderpath槽连接起来了
        # 因为MainForm继承于上面的，所以槽就是MainForm.getfolderpath()
        # 信号和槽的绑定尽量放在__init__中
        dir_choose = QFileDialog.getExistingDirectory(self, "选取文件夹", os.getcwd())
        if dir_choose == "":
            self.label_msg.setText("已取消选择文件夹")
            return
        self.label_msg.setText("你选择的文件夹为:" + dir_choose)
        t = threading.Thread(target=getdatabyexcel, args=(dir_choose,), name='funciton')
        t.start()
    
    def updatelabel(self, msg):
        self.label_msg.setText(msg)

if __name__ == "__main__":
    app = QApplication(sys.argv)
    example = Example()
    mainForm = MainForm()
    mainForm.show()
    sys.exit(app.exec_())