# 1.0
FROM ubuntu:16.04
MAINTAINER linrong <lazyhlr@gmail.com>
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
# change the source list
COPY sources.list /etc/apt/
RUN apt-get update && \
    apt-get install -y  openssh-server && \
    apt-get install -y  git  && \
    apt-get install -y  sudo  && \
    apt-get install -y  vim  && \
    apt-get install -y  wget && \
    apt-get install -y  iputils-ping && \
    apt-get install -y  net-tools && \
    apt-get install -y  gcc && \
    apt-get install -y  zlib* && \
    apt-get install -y  unzip  && \
    apt-get install -y  make && \
    apt-get install -y  libbz2-dev && \
    apt-get install -y  libssl-dev && \
    apt-get install -y  libsqlite3-dev && \
    apt-get install -y  lsof && \
    apt-get install -y  virtualenv && \
    rm -rf /var/lib/apt/lists/* 
RUN cd /home && \
    wget https://www.python.org/ftp/python/3.6.5/Python-3.6.5.tgz && \
    tar xfz Python-3.6.5.tgz && \
    cd Python-3.6.5/ && \
    ./configure --prefix=/usr/bin/python3.6 --enable-optimizations && \
    make && \
    make install && \
    # rm /usr/bin/python && \
    ln -s /usr/bin/python3.6/bin/python3.6 /usr/bin/python && \
    # rm /usr/bin/pip && \
    ln -s /usr/bin/python3.6/bin/pip3.6 /usr/bin/pip
RUN mkdir /var/run/sshd
RUN useradd --create-home --no-log-init --shell /bin/bash python && \
    adduser python sudo && \
    echo 'python:python' | chpasswd && \
    echo 'root:root' | chpasswd
RUN sed -i 's/PermitRootLogin prohibit-password/PermitRootLogin yes/' /etc/ssh/sshd_config

# SSH login fix. Otherwise user is kicked off after login
RUN sed 's@session\s*required\s*pam_loginuid.so@session optional pam_loginuid.so@g' -i /etc/pam.d/sshd

ENV NOTVISIBLE "in users profile"
RUN echo "export VISIBLE=now" >> /etc/profile

USER python
WORKDIR /home/

EXPOSE 22 80 8080 8000
CMD ["/usr/sbin/sshd", "-D"]