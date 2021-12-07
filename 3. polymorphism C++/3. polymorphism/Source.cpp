//Êîìïëåêñíûå ÷èñëà 


#include <stdio.h>
#include <windows.h>
#include <locale.h>

class class1 {

};

class complex_numbers { 
	float a;
	float b;

public: complex_numbers(float a = 0, float b = 0) {
		this->a = a; 
		this->b = b; 
	} 
	
	complex_numbers operator + (complex_numbers&); //1. ñëîæåíèå êîìïëåêñíûõ ÷èñåë

	complex_numbers operator * (complex_numbers&); //2. óìíîæåíèå êîìïëåêñíûõ ÷èñåë

	complex_numbers& operator + (void); //3. ñìåíà çíàêà ìíèìîé ÷àñòè

	complex_numbers& operator ++ (void); //4. ïîñòôèêñ èíêðåìåíò

	complex_numbers operator ++ (int); //5. ïðåôèêñ èíêðåìåíò

	void show(void); 
};
//1
complex_numbers complex_numbers::operator + (complex_numbers& c1) {	
	complex_numbers sum = complex_numbers(this->a + c1.a, this->b + c1.b);
	return sum;
}
//2
complex_numbers complex_numbers::operator * (complex_numbers& c2) {
	complex_numbers mult = complex_numbers(this->a * c2.a - this->b * c2.b, this->b * c2.a + this->a * c2.b);
	return mult;
}
//3
complex_numbers& complex_numbers::operator + (void) {
	b = -b; return *this;
}
//4 
complex_numbers& complex_numbers::operator ++ (void) {
	++a; return *this;
}
//5
complex_numbers complex_numbers::operator ++ (int) {
	complex_numbers z(this->a, this->b); ++a;
	return z;
}

void complex_numbers::show(void) {
	//ëèøíèé "-" ïåðåä ìíèìîé ÷àñòüþ
	if (b < 0) printf("\n%.2f%.2fi", a, b);
	else printf("\n%.2f+%.2fi", a, b);
}

int main(void) { 
	setlocale(LC_ALL, "Russian");
	//ñëîæåíèå
	complex_numbers com1(1, -1);
	complex_numbers com2(2, -1);
	complex_numbers com_sum = com1 + com2;
	printf("\n\nÏåðâîå ÷èñëî"); com1.show();
	printf("\nÂòîðîå ÷èñëî"); com2.show();
	printf("\nÑóììà ïåðâîãî è âòîðîãî"); com_sum.show();
	//óìíîæåíèå
	complex_numbers com_mult = com1 * com2;
	printf("\nÏðîèçâåäåíèå ïåðâîãî âòîðîãî"); com_mult.show();
	//çíàê ìíèìîé ÷àñòè
	com1 = +com1;
	printf("\nÑìåíà çíàêà ìíèìîé ÷àñòè ïåðâîãî ÷èñëà"); com1.show();
	//ïîñòôèêñ
	printf("\nÂûïîëíåíèå com3=com1++");
	complex_numbers com3 = com1++;
	printf("\ncom3="); com3.show();
	printf("\ncom1="); com1.show();
	//ïðåôèêñ
	printf("\nÂûïîëíåíèå com4=++com2");
	complex_numbers com4 = ++com2;
	printf("\ncom4="); com4.show();
	printf("\ncom2="); com2.show();

	printf("\n"); system("pause");  
	return 0;
}
