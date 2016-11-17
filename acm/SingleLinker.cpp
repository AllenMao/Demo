//20161117 The interview questions

#include <iostream>
using namespace std;

//define struct of node
typedef struct stu
{
	int id;
	struct stu *next;
}node;

node* createNode()
{
	node *head, *p, *s;
	int x,cycle = 1;
	head = (node*)malloc(sizeof(node));//head node
	p = head;

	printf("Please input the id(with 0 end):");
	while(cycle)
	{
		
		scanf("%d", &x);
		if(x!= 0)
		{
			s = (node*)malloc(sizeof(node));//new node
			s->id = x;
			p->next = s;
			p = s;
		}
		else
		{
			cycle = 0;
		}

	}
	p->next = NULL;
	printf("Create linker successfully!\n");

	return head;
}

int getLength(node *head)
{
	if(head == NULL) {puts("null point");return 0;}

	int n = 0;
	node *p;
	p = head->next;//No statisticing head node
	while(p != NULL)
	{
		p = p->next;
		n++;
	}

	return n;
}

void print(node *head)
{
	if(head == NULL) {puts("null point");return;}

	node *p;
	int n;
	n = getLength(head);
	p = head->next;

	printf("The Linker's %d datas:",n);

	while(p != NULL)
	{
		printf(" %d", p->id);
		p = p->next;
	}
	printf("\n");
}

node *remove(node *head ,int num)
{
	if(head == NULL) {puts("null point");return NULL;}

    node *p1,*p2;
	p2 = head;
	p1= p2->next;

    while(num != p1->id && p1->next!=NULL)//search id=num node
    {
        p2 = p1;
        p1=p1->next;
    }
    if(num == p1->id) //found
    {
		p2->next=p1->next;
		printf("id = %d's node have removed\n",num);
    }
    else
    {
        printf("id = %d's node could not been found\n",num);
    }
    return head;
}

node *insert(node *head, int num)
{
	if(head == NULL) {puts("null point");return NULL;}

    node *p0, *p1, *p2;
	p2 = head;
	p1 = head->next;
    p0 = (node *)malloc(sizeof(node));
    p0->id = num;

	while(p0->id > p1->id && p1->next != NULL)
    {
        p2 = p1;
        p1 = p1->next;
    }
	if(p0->id <= p1->id)
    {
		p2->next = p0;
		p0->next = p1;
    }
	else
    {
        p1->next = p0;
        p0->next = NULL;
    }
	printf("id = %d's node have inserted\n",num);

	return head;
}

int main()
{
	node *head;

	head = createNode();
	print(head);
	remove(head, 3);
	print(head);
	insert(head, 3);
	print(head);

	system("pause");
	return 0;
}