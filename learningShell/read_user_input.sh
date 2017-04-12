#! /bin/bash

#user input

echo "Enter name : "
read name

echo "Enter name : $name"

read name1 name2 name3	#multiple variable
echo "Enter name : $name1, $name2, $name3"

read -p 'username: ' user_var	#label and variable in same line
read -p 'password: ' upss_var
echo "username: $user_var"

echo "enter names : "
read -a names #array
echo "Names: ${names[0]}, ${names[1]}, ${names[2]}"