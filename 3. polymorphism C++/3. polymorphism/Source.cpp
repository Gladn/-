//Комплексные числа 
//https://ru.wikipedia.org/wiki/%D0%9A%D0%BE%D0%BC%D0%BF%D0%BB%D0%B5%D0%BA%D1%81%D0%BD%D0%BE%D0%B5_%D1%87%D0%B8%D1%81%D0%BB%D0%BE
//https://ru.wikipedia.org/wiki/%D0%9E%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%BE%D1%80%D1%8B_%D0%B2_C_%D0%B8_C%2B%2B

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
	
	complex_numbers operator + (complex_numbers&); //1. сложение комплексных чисел

	complex_numbers operator * (complex_numbers&); //2. умножение комплексных чисел

	complex_numbers& operator + (void); //3. смена знака мнимой части

	complex_numbers& operator ++ (void); //4. постфикс инкремент

	complex_numbers operator ++ (int); //5. префикс инкремент

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
	//лишний "-" перед мнимой частью
	if (b < 0) printf("\n%.2f%.2fi", a, b);
	else printf("\n%.2f+%.2fi", a, b);
}

int main(void) { 
	setlocale(LC_ALL, "Russian");
	//сложение
	complex_numbers com1(1, -1);
	complex_numbers com2(2, -1);
	complex_numbers com_sum = com1 + com2;
	printf("\n\nПервое число"); com1.show();
	printf("\nВторое число"); com2.show();
	printf("\nСумма первого и второго"); com_sum.show();
	//умножение
	complex_numbers com_mult = com1 * com2;
	printf("\nПроизведение первого второго"); com_mult.show();
	//знак мнимой части
	com1 = +com1;
	printf("\nСмена знака мнимой части первого числа"); com1.show();
	//постфикс
	printf("\nВыполнение com3=com1++");
	complex_numbers com3 = com1++;
	printf("\ncom3="); com3.show();
	printf("\ncom1="); com1.show();
	//префикс
	printf("\nВыполнение com4=++com2");
	complex_numbers com4 = ++com2;
	printf("\ncom4="); com4.show();
	printf("\ncom2="); com2.show();

	printf("\n"); system("pause");  
	return 0;
}