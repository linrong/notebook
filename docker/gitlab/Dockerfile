FROM docker:latest
MAINTAINER linrong <lazyhlr@gmail.com>
RUN echo http://mirrors.ustc.edu.cn/alpine/v3.9/main > /etc/apk/repositories;echo http://mirrors.ustc.edu.cn/alpine/v3.9/community >> /etc/apk/repositories
RUN apk add --no-cache py-pip python-dev libffi-dev openssl-dev gcc libc-dev make
RUN pip install docker-compose -i https://mirrors.aliyun.com/pypi/simple/

