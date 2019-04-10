# V1.0
FROM python:3.6.5
MAINTAINER linrong <lazyhlr@gmail.com>
ENV PYTHONUNBUFFERED 1
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
COPY sources.list /etc/apt/
RUN apt-get -y upgrade
RUN apt-get update 
RUN apt-get install -y build-essential && \
    apt-get install -y libldap2-dev libsasl2-dev ldap-utils python-tox  lcov valgrind vim virtualenv && \
    rm -rf /var/lib/apt/lists/*
RUN mkdir /code
WORKDIR /code
