test:main.cpp add.o sub.o
	gcc main.cpp add.o sub.o -o test
add.o:add.cpp add.h
	gcc -c add.cpp
sub.o:sub.cpp sub.h
	gcc -c sub.cpp
clean:
	rm -f test *.o