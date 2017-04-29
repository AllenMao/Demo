#read_file.sh

#	-f checking file exists
#	-d checking director exists
#	-s checking file is empty or not
echo -e "Enter the name of the file : \c"

read file_name

if [  -e $file_name ] #use -e to ensure file exists. 
then
	echo "$file_name found"
else
	echo "$file_name not found"
fi