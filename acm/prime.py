#coding:utf8
'''
 The movie "Ex Machina" about prime sieve method
'''
import sys

def sieve(n):
    #compute primes using sieve eratosthenes
    x = [1] * n
    x[1] = 0

    for i in range(2,n/2):
        j = 2 * i
        while j < n:
            x[j] = 0
            j = j + i
    return x

def prime(n,x):
    #find n th prime
    i = 1
    j = 1
    while j <= n:
        if x[i] == 1:
            j = j + 1
        i = i + 1
    return i-1

x = sieve(10000)

code = [1206,301,384,5]
key = [1,1,2,2]
#print "".join('abcd'+chr(22))

#print ISBN = ,ASCLL to char
sys.stdout.write("".join(chr(i) for i in [73,83,66,78,32,61,32]))


for i in range(0,4):
    sys.stdout.write(str(prime(code[i],x)-key[i]))
print ""