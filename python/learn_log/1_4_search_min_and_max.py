import heapq

# 查找方案
# 集合中查找最小的或者最大的N个元素
nums = [1, 7, 8, 5, 34, 35, 75, 1, 45, 23, 454, 12, 9, 43]

# 1
# 如果查找的N>1但又不会太大时，使用函数 nlargest() 和 nsmallest() 是很合适的
print(heapq.nlargest(3, nums))
print(heapq.nsmallest(3, nums))

portfolio = [
    {'name': 'IBM', 'shares': 100, 'price': 91.1},
    {'name': 'AAPL', 'shares': 50, 'price': 543.22},
    {'name': 'FB', 'shares': 200, 'price': 21.09},
    {'name': 'HPQ', 'shares': 35, 'price': 31.75},
    {'name': 'YHOO', 'shares': 45, 'price': 16.35},
    {'name': 'ACME', 'shares': 75, 'price': 115.65}
]

cheap = heapq.nsmallest(3, portfolio, key=lambda s: s['price'])
expensive = heapq.nlargest(3, portfolio, key=lambda s: s['price'])
print(cheap)
print(expensive)

# 2
# 如果N=1时，min和max合适
print('--------2--------')
print(min(nums))
print(max(nums))

# 3
# 如果 N 的大小和集合大小接近的时候，通常先排序这个集合然后再使用切片操作会更快点
print('-------3---------')
print(sorted(nums)[:6])
print(sorted(nums)[-6:])

# 4
# 另一种排序
print('--------4--------')
heap = list(nums)
# 将数据排序然后放进一个列表中
heapq.heapify(heap)
print(heap)
print(type(heap))

# 提取最小的元素,操作的时间复杂度为 O(log N)，N 是堆大小，有点像把列表队列处理了
print(heapq.heappop(heap))
print(heap)
print(heapq.heappop(heap))
print(heap)