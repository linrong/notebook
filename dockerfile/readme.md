### description
how to bulid ubuntu16.04 with ssh

### use
docker build -t linrong/ubuntu:1.0 .
docker run -d -P --name ubuntu_dev linrong/ubuntu:1.0
docker port ubuntu_dev