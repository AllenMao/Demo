#include<stdio.h>

int add()
{
    freopen("in.txt","r",stdin);
    int t,a,b;
    scanf("%d",&t);
    while(t--)
    {
         scanf("%d%d",&a,&b);
         
	 printf("a + b = %d\n",a+b);
    }
    return 0;
}
