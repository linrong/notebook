# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'mainwindown.ui'
#
# Created by: PyQt5 UI code generator 5.13.2
#
# WARNING! All changes made in this file will be lost!


from PyQt5 import QtCore, QtGui, QtWidgets


class Ui_MainWindow(object):
    def setupUi(self, MainWindow):
        MainWindow.setObjectName("MainWindow")
        MainWindow.resize(373, 210)
        self.centralwidget = QtWidgets.QWidget(MainWindow)
        self.centralwidget.setObjectName("centralwidget")
        self.Button_folder = QtWidgets.QPushButton(self.centralwidget)
        self.Button_folder.setGeometry(QtCore.QRect(140, 50, 81, 31))
        self.Button_folder.setObjectName("Button_folder")
        self.label_msg = QtWidgets.QLabel(self.centralwidget)
        self.label_msg.setGeometry(QtCore.QRect(20, 120, 331, 31))
        self.label_msg.setLayoutDirection(QtCore.Qt.LeftToRight)
        self.label_msg.setAlignment(QtCore.Qt.AlignCenter)
        self.label_msg.setObjectName("label_msg")
        MainWindow.setCentralWidget(self.centralwidget)

        self.retranslateUi(MainWindow)
        self.Button_folder.clicked.connect(MainWindow.getfolderpath)
        QtCore.QMetaObject.connectSlotsByName(MainWindow)

    def retranslateUi(self, MainWindow):
        _translate = QtCore.QCoreApplication.translate
        MainWindow.setWindowTitle(_translate("MainWindow", "Excel处理"))
        self.Button_folder.setText(_translate("MainWindow", "选择文件夹"))
        self.label_msg.setText(_translate("MainWindow", "请选择文件夹进行处理excel"))
