### description
how to bulid ubuntu16.04 with ssh

### use
docker build -t linrong/ubuntu:1.0 .
docker run -d -P --name ubuntu_dev linrong/ubuntu:1.0
docker port ubuntu_dev

### 2.0
```
sudo apt-get update
# root:root@mysql
sudo apt-get install -y mysql-server
sudo service mysql start
sudo service mysql status
vim /etc/mysql/my.cnf
# 修改默认编码
[mysqld]
character_set_server=utf8
init_connect='SET NAMES utf8'
#
sudo service mysql restart
sudo mysql -uroot -p
# 查看数据库编码
show variables like '%character%';
# 创建数据库用户dgbuaa,密码dgbuaa@123.cn
create user 'dgbuaa'@'localhost' identified by 'dgbuaa@123.cn';
```