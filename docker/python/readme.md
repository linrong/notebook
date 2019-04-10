## Ubuntu16.04
### description
how to bulid ubuntu16.04 with ssh

[docker hub](https://hub.docker.com/r/linrong/ubuntu/)
### use
docker build -t linrong/ubuntu:1.0 .
docker run -d -P --name ubuntu_dev linrong/ubuntu:1.0
docker port ubuntu_dev

### 1.1（目前一般情况下不使用1.1，基于一个容器一个应用）
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
## debian
### python3.6.5
> 目前已不使用上面的ubuntu,修改为使用debian

[docker hub](https://cloud.docker.com/repository/docker/linrong/python3)