import subprocess
import time
import os
import sys

def get_git_revision_hash():
    return subprocess.check_output(['git', 'rev-parse', 'HEAD']).decode('ascii').strip()

def get_git_revision_short_hash():
    return subprocess.check_output(['git', 'rev-parse', '--short', 'HEAD']).decode('ascii').strip()

def get_git_revision_branchs():
    return subprocess.check_output(['git', 'rev-parse', '--symbolic', '--branches']).decode('ascii').strip()

def get_git_revision_branch():
    return subprocess.check_output(['git', 'rev-parse', '--abbrev-ref', 'HEAD']).decode('ascii').strip()

def get_git_revision_tags():
    return subprocess.check_output(['git', 'tag', '--points-at', 'HEAD']).decode('ascii').strip()

def make_c_software(url_dir,url_file):
    if not os.path.exists(url_dir):
        os.mkdir(url_dir)
    lines=['#include \"software_version.h\"\n',
         '\n',
         'const char commit_id[] = '+"\""+hash+"\";    /*!< give the the commit id name  */\n",
         'const char branch_name[] = '+"\""+branch+"\";    /*!< give them the branch name */\n",
         'const uint32_t time_stamp = '+str(timestamp)+";    /*!< give them the timestamp */\n",
         'const char version_name[] = '+"\""+tags+"\";    /*!< the code version */\n",
         '\n',
         'const char *get_software_commit_id(void)\n',
         '{\n',
         '	return commit_id;\n',
         '}\n',
         'const char *get_software_branch(void)\n',
         '{\n',
         '	return branch_name;\n',
         '}\n',
         'const uint32_t get_timestamp(void)\n',
         '{\n',
         '	return time_stamp;\n',
         '}\n',
         'const char *get_version_name(void)\n',
         '{\n',
         '	return version_name;\n',
         '}\n']
    with open(url_file, 'w+') as f:
        f.writelines(lines)

def make_h_software(url_dir,url_file):
    if not os.path.exists(url_dir):
        os.mkdir(url_dir)
    lines=['#ifndef __SOFTWARE_VERSION_H\n',
         '\n',
         '#define __SOFTWARE_VERSION_H\n',
         '\n',
         '#include <stdint.h>\n',
         '\n',
         'const char *get_software_commit_id(void);\n',
         'const char *get_software_branch(void);\n',
         'const uint32_t get_timestamp(void);\n',
         'const char *get_version_name(void);\n',
         '\n',
         '#endif',
         '\n']
    with open(url_file, 'w+') as f:
        f.writelines(lines)


def remake_software(url_file):
    data=''
    with open(url_file, 'r+', encoding='utf-8') as f:
        # for line in f.readlines():
        #     if line.find('const char commit_id[]')>=0:
        #         line='const char commit_id[] = '+"\""+hash+"\";    /*!< give the the commit id name  */\n"
        #     elif line.find('const char branch_name[]')>=0:
        #         line='const char branch_name[] = '+"\""+branch+"\";    /*!< give them the branch name */\n"
        #     elif line.find('const uint32_t time_stamp')>=0:
        #         line='const uint32_t time_stamp = '+"\""+str(timestamp)+"\";    /*!< give them the timestamp */\n"
        #     elif line.find('const char version_name[]')>=0:
        #         line='const char version_name[] = '+"\""+tags+"\";    /*!< the code version */\n"
        #     data+=line
        data=f.readlines()
        data[2]='const char commit_id[] = '+"\""+hash+"\";    /*!< give the the commit id name  */\n"
        data[3]='const char branch_name[] = '+"\""+branch+"\";    /*!< give them the branch name */\n"
        data[4]='const uint32_t time_stamp = '+str(timestamp)+";    /*!< give them the timestamp */\n"
        data[5]='const char version_name[] = '+"\""+tags+"\";    /*!< the code version */\n"
    
    with open(url_file, 'w+') as f:
        f.writelines(data)

if __name__ == "__main__":
    hash=get_git_revision_short_hash() if get_git_revision_short_hash() else 'null'
    print('hash:'+hash)
    branch=get_git_revision_branch() if get_git_revision_branch() else 'null'
    print('branch:'+branch)
    tags=get_git_revision_tags() if get_git_revision_tags() else 'null'
    print('tag:'+tags)
    timestamp=int(time.time())
    print('time:'+str(timestamp))

    # 查询文件进行添加
    url_c_dir=sys.path[0]+'/Src/'
    url_h_dir=sys.path[0]+'/Inc/'
    print('software_version.c:'+url_c_dir)
    print('software_version.h:'+url_h_dir)
    url_c_file=url_c_dir+'software_version.c'
    url_h_file=url_h_dir+'software_version.h'
    if not os.path.exists(url_h_file):
        print('make software_version.h!')
        make_h_software(url_h_dir,url_h_file)
    if not os.path.exists(url_c_file):
        print('make software_version.c!')
        make_c_software(url_c_dir,url_c_file)
    else:
        remake_software(url_c_file)