#pass_args.sh

echo $0 $1 $2 $3 '> echo $1 $2 $3'

args=("$@")		#get args

echo ${args[0]} ${args[1]} ${args[2]}

echo $@		#print all args

echo $#		#print the number of args