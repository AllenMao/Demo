//20161117 The interview questions

#include <iostream>
using namespace std;

//define struct of node
typedef struct stu
{
	int id;
	struct stu *next;
}node;

//have head node
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

//get length of link
int getLength(node *head)
{
	if(head == NULL || head->next == NULL) {puts("null point or no data");return 0;}

	int n = 0;
	node *p;
	p = head->next;//No statistic head node
	while(p != NULL)
	{
		p = p->next;
		n++;
	}

	return n;
}

//view
void print(node *head)
{
	if(head == NULL || head->next == NULL) {puts("null point or no data");return;}

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
	if(head == NULL || head->next == NULL) {puts("null point or no data");return NULL;}//no data

    node *p1,*p2;
	p2 = head;
	p1= p2->next;

    while(p1->next!=NULL && num != p1->id)//search id=num node
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
	if(head == NULL || head->next == NULL) {puts("null point or no data");return NULL;}

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

//bubbling sort
node *sort(node *head)
{
	if(head == NULL || head->next == NULL) {puts("null point or no data");return NULL;}

	node *p, *p2, *p3;
	int n, temp;
	n = getLength(head);

	for(int i = 0; i < n-1; i++)
	{
		p = head->next;
		for(int j = 0; j < n-1-i; j++)
		{
			if(p->id > p->next->id)
			{
				temp = p->id;
				p->id = p->next->id;
				p->next->id = temp;
			}
			p = p->next;
		}
	}

	return head;
}

node *reverse(node *head)
{
	if(head == NULL || head->next == NULL) {puts("null point or no data");return head;}

	node *p1, *p2, *p3;
	p1 = head->next;
	p2 = p1->next;

	while(p2)
	{
		p3 = p2->next;
		p2->next = p1;
		p1 = p2;
		p2 = p3;
	}
	head->next = p2;
	//head = p1;

	return head;
}

//perfect way
void searchmid(node *head, node *mid)
{
	if(head == NULL || head->next == NULL) {puts("null point or no data");return;}

    node *p, *q;
	p=head->next;
    q = p;
	while(p->next->next != NULL)
    {
        p = p->next->next;
        q = q->next;
        mid = q;
    }
}

node *searchLastElement(node *head, int m)
{
	if(head == NULL || head->next == NULL) {puts("null point or no data");return NULL;}
	node *p, *q;
	p = head->next;
	for(int i = 0; i< m-1; i++)
	{
		if(p->next != NULL)
			p = p->next;
		else
		{
			printf("no exit last m-th node!\n");
			return NULL;
		}
	}

	q = head->next;
	while(p->next != NULL)
	{
		p = p->next;
		q = q->next;
	}

	return q;
}

//free memory
void freeMemory(node *head)
{
	node *temp, *p = head;

	while(p != NULL)
	{
		temp = p->next;
		free(p);
		p = temp;
	}
}

int main()
{
	node *head, *q;

	head = createNode();

	sort(head);
	print(head);
	
	remove(head, 3);
	print(head);

	insert(head, 3);
	print(head);

	q = searchLastElement(head, 2);
	printf("%d\n", q->id);

	freeMemory(head);

	system("pause");
	return 0;
}