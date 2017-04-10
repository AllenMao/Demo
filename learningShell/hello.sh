#! /bin/bash
#1.this is a comment
echo "Hello World" #2.this is also a comment. 

#Learning variables

##System variables: This type of variable defined in CAPTIAL LETTERS.
echo Our shell name is $BASH
echo Our shell version name is $BASH_VERSION
echo Our home directory is $HOME
echo Our current working directory is $PWD

##User defined variables: This type of variable defined in lower letters.
name=Mark	#no space in left and right of '='
VALUE=10
10VAL=9	#error:the prefix cannt be number  

echo The name is $name
echo The value is $VALUE
echo The value is $10VAL	#error
