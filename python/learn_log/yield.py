'''
如果一个函数包含 yield 表达式，那么它是一个生成器函数；调用它会返回一个特殊的迭代器，称为生成器
'''
# 1
def test():
    yield 1
t= test()
print(next(t))
# print(next(t)) # 这里会报错,因为生成器已经运行完了,可以使用try except StopIteration:

# 2
def foo():
    print("starting...")
    while True:
        res = yield 4
        print("res:",res)
g = foo()
print(next(g))
print("*"*20)
print(next(g))
print("*"*20)
print(g.send(7))

# 3
def dedupe(items, key=None):
    seen = set()
    for item in items:
        val = item if key is None else key(item)
        if val not in seen:
            yield item
            seen.add(val)

a = [ {'x':1, 'y':2}, {'x':1, 'y':3}, {'x':1, 'y':2}, {'x':2, 'y':4}]
print(dedupe(a, key=lambda d: (d['x'],d['y'])))
# 这个说明了使用yield是会构建一个生成器
# 但list()会一直运行这个生成器，导致最后生成一个数组
print(list(dedupe(a, key=lambda d: (d['x'],d['y']))))
