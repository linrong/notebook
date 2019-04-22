### gitlab IC的镜像

> [dockerhub](https://cloud.docker.com/u/linrong/repository/docker/linrong/docker-gitlab)

#### 1.0
```bash
docker build -t linrong/docker-gitlab:1.0 .
# 这是一个安装了docker-compose的容器，但单独在容器里面使用docker目前测试是不允许的
# 运行启动或者安装在启动docker都失败，可能要使用docker:dind才可以在docker里面启动docker
# 这个镜像的使用方式应该如下(应该和docker:dind一起使用，然后gitlab runner也要register为dockers in docker模式https://docs.gitlab.com/ce/ci/docker/using_docker_build.html#use-docker-in-docker-executor)
# gitlab.yml:
# docker镜像
image: linrong/docker-gitlab:1.0
variables:
  # When using dind service we need to instruct docker, to talk with the
  # daemon started inside of the service. The daemon is available with
  # a network connection instead of the default /var/run/docker.sock socket.
  #
  # The 'docker' hostname is the alias of the service container as described at
  # https://docs.gitlab.com/ee/ci/docker/using_docker_images.html#accessing-the-services
  #
  # Note that if you're using Kubernetes executor, the variable should be set to
  # tcp://localhost:2375 because of how Kubernetes executor connects services
  # to the job container
  DOCKER_HOST: tcp://docker:2375/
  # When using dind, it's wise to use the overlayfs driver for
  # improved performance.
  DOCKER_DRIVER: overlay2
# 依赖的docker服务
services:
  - docker:dind
# 开始执行脚本前所需执行脚本
before_script:
  - docker info
  - docker-compose -verision
# 脚本执行完后的钩子，执行所需脚本
# after_script:
#   - rm secrets
# 该ci pipeline适合的场景
stages:
  - build
  - test
  - deploy
# 定义的任务1
test1:
  # 场景为构建
  stage: test
  # 所需执行的脚本
  script:
    - echo "Testing the app"
    - docker-compose rm -f
    - docker-compose -f docker-compose.test.yml up
    - docker-compose -f docker-compose.test.yml stop
  # # 在哪个分支上可用
  # only:
  #   - master
  # # 指定哪个ci runner跑该工作
  # tags:
  #   - docker
```