'''
Python 中定义函数有两种方法，一种是用常规方式 def 定义，函数要指定名字，第二种是用 lambda 定义，不需要指定名字，称为 Lambda 函数。

Lambda 函数又称匿名函数，匿名函数就是没有名字的函数，函数没有名字也行？当然可以啦。有些函数如果只是临时一用，而且它的业务逻辑也很简单时，就没必要非给它取个名字不可。

关键字lambda表示匿名函数，冒号前面的x表示函数参数。

匿名函数有个限制，就是只能有一个表达式，不用写return，返回值就是该表达式的结果。

用匿名函数有个好处，因为函数没有名字，不必担心函数名冲突。此外，匿名函数也是一个函数对象，也可以把匿名函数赋值给一个变量，再利用变量来调用该函数
'''
print('----------lambda---------')
print(type(lambda x, y: x + y))
add = lambda x, y: x + y
print(add)
print((lambda x, y: x + y)(1, 2))
print(add(1, 2))

print('----------sorted---------')
# 一个整数列表，要求按照列表中元素的绝对值大小升序排列
list1 = [3, 5, -4, -1, 0, -2, -6]
print(sorted(list1, key=lambda x: abs(x)))

# 过滤
# filter() 函数用于过滤序列，过滤掉不符合条件的元素，返回由符合条件元素组成的新列表。
# 该接收两个参数，第一个为函数，第二个为序列，序列的每个元素作为参数传递给函数进行判断，然后返回 True 或 False，最后将返回 True 的元素放到新列表中。
print('----------filter---------')
print(list(filter(lambda x: x % 3 == 0, list1)))

# map
# map() 会根据提供的函数对指定序列做映射。
# 第一个参数 function 以参数序列中的每一个元素调用 function 函数，返回包含每次 function 函数返回值的新列表。
print('-----------map----------')
print(list(map(lambda x: x * 2 + 1, list1)))

# reduce,py3已从全局名字空间移除
# reduce() 函数会对参数序列中元素进行累积
# 函数将一个数据集合(链表，元组等)中的所有数据进行下列操作:用传给reduce中的函数function(有两个参数)先对集合中的第1,2个元素进行操作,得到的结果再与第三个数据用function函数运算,最后得到一个结果
from functools import reduce
print('----------reduce--------')
print(reduce(lambda x, y: x + y, list1))


# zip
# zip（）的目的是映射多个容器的相似索引，以便它们可以仅作为单个实体使用。
# 基础语法：zip(*iterators); 参数：iterators为可迭代的对象，例如list，string; 返回值：返回单个迭代器对象，具有来自所有容器的映射值
print('-----------zip----------')
keys = ['name','age']
values = ['xiaobai',18]
my_dict = dict(zip(keys,values))
print(my_dict)

name = [ "xiaobai", "john", "mike", "alpha" ]
age = [ 4, 1, 3, 2 ]
marks = [ 40, 50, 60, 70 ]
mapped = list(zip(name, age, marks))
print(mapped)

names, ages, marks = zip(*mapped)
print(names)