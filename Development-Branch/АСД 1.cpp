#define _CRT_SECURE_NO_WARNINGS
#include "stdlib.h"
#include "stdio.h"
#include "conio.h"
#include "math.h"
#include "locale.h"
#include "string.h"
#include "windows.h"
#include "time.h"

void choice(int n, int* arr); //метод сортування вибору

void shell(int n, int* arr); //метод сортування Шелла

void count(int n, int* arr); //метод сортування вибору

void show_time(double time); //відображення часу

void input_from_arr(int* arr, int* saved_arr, int n); // введеня збереженного початкового масиву до поточного

void comparson(int n, int fill, int* arr, FILE* file, int* saved_arr); // функція вибору одразу всіх методів сортування з кроком

int choice_sort(int n, int fill, int* arr, FILE* file, int* saved_arr); // інтерфейсна частина програми(вибір методу сортування)

int random_fill_boarders(int i); // введення верхньої та нижньої границі для заповнення масиву випадковими числами

void saved_array(int* arr, int n, FILE* fp); // збереження масиву у додатковий масив

int fill_array(int n, int fill, int* arr, int* saved_arr, FILE* fp); // інтерфейсна частина програми(метод заповнення масиву)

void file_print(int* arr, int n, FILE* file); // запис у файл поточного масиву

int enter_size(); // введення розміру масиву

int errors(int flag, int n); // "Функція помилок"

int main() // Головне тіло програми та інтерфейсна частина
{
    FILE* file, *fp;
    int* arr = NULL, *saved_arr = NULL; // Динамічний масив
    int fill = 0, n; // n - розмір масиву, fill - змінна вибору користувача, flag - змінна перевірки
    SetConsoleCP(1251); // Локалізація
    SetConsoleOutputCP(1251);

    srand(time(NULL)); // Встановлення початку послідовності для функції rand() від часу роботи програми time
    
    if ((file = fopen("Вихідне значення.txt", "w")) == NULL || ((fp = fopen("Збережений невідсортований масив.txt", "w+")) == NULL)) //Відкриття вхідного файлу з перевіркой
    {
        return 0;
    }

    do {
        if (fill != 1) {

            n = enter_size();

            if (errors(0, n) == 0)
                return 0;

            printf("Введіть\n 1 для заповнення масиву автоматично\n 2 для заповнення вручну \n 3 для вводу з файлу \n 4 для виходу із програми : ");

            scanf_s("%d", &fill);

            if (fill == 4)
                return 0;

            if (!(arr = (int*)malloc(n * sizeof(int))) || !(saved_arr = (int*)malloc(n * sizeof(int)))) { // Виділення пам'яті під динамічний масив
                printf("Для такого масиву не вистачає пам'яті!");
                return 0;
            }

            if (errors(fill_array(n, fill, arr, saved_arr, fp), n) == 0)  // Виклик функції заповнення масивів з перевіркою на помилки
                return 0;

        }
        printf("Введіть\n 1 для сортування методом вибору\n 2 для сортування методом Шелла\n 3 для сортування методом підрахунків\n 4 для сортування для порівняння\n 5 для виходу із програми : ");

        scanf_s("%d", &fill);

        if (fill == 5)
            return 0;

        if (errors(choice_sort(n, fill, arr, file, saved_arr), n) == 0)  // Виклик функції заповнення масивів з перевіркою на помилки
            return 0;

        printf("Чи бажаєте ви продовжити роботу з цим масивом ?\n1 Так\n2 Ні : ");

        scanf_s("%d", &fill);

    } while (fill != 5);

    if (fclose(file) == EOF || fclose(fp) == EOF)    //Перевірка на закриття файлу з закриттям
    {
        return 4;
    }

    free(arr);

    return 0;
}

void choice(int n, int* arr) //метод сортування вибору
{
    for (int i = 0, potoch, min; i < n - 1; i++) // головний цикл
    {
        min = i; // тимчасова змінна
        for (int j = i + 1; j < n; j++) // цикл знаходження найменшого элементу
        {
            if (arr[j] < arr[min]) // умова(якщо наступний елемент менший попереднього)
            {
                min = j; // тимчасова змінна = номеру наступного елементу
            }
        }
        if (arr)
        {
            potoch = arr[min]; // тимчасова змінна = найменшому елементу
            arr[min] = arr[i]; // найменший елемент = поточному елементу 
            arr[i] = potoch; // поточний елемент = найменшому елементу
        }
    }
}

void shell(int n, int* arr) //метод сортування Шелла
{
    for (int step = n / 2; step > 0; step /= 2) // цикл визначення "шагу"
        for (int i = step; i < n; i++) // цикл "шагу"
            for (int j = i - step, temp; j >= 0 && arr[j] > arr[j + step]; j -= step) // цикл сортування
            {
                temp = arr[j]; //тимчасова змінна = поточний елемент
                arr[j] = arr[j + step]; //поточний елемент = елемент з шагом
                arr[j + step] = temp; //елемент з шагом = поточному елементу
            }
}

void count(int n, int* arr) //метод сортування вибору
{
    int  min = 0, max = 0; // максимальне та мінімальне значення
    int sup_arr_size; // розмір допоміжного масиву
    int* sup_arr, * sort_sup_arr; // покажчик на додатковий масив та на масив відсортованої послідовності

    for (int i = 1; i < n; i++) { //цикл знаходження максимального та мінімального елемента
        if (arr[i] < min)
            min = arr[i];
        if (arr[i] > max)
            max = arr[i];
    }

    sup_arr_size = max - min + 1; // знаходження розміру допоміжного масиву

    sup_arr = (int*)(malloc(sup_arr_size * sizeof(int))); // виділення пам'яті для додаткового масиву

    sort_sup_arr = (int*)(malloc(n * sizeof(int))); // виділення пам'яті для масиву відсортованої послідовності

    memset(sup_arr, 0, sup_arr_size * sizeof(int)); // заповненная масиву нулями

    memset(sort_sup_arr, 0, n * sizeof(int));  // заповненная масиву нулями

    for (int i = 0; i < n; i++) // цикл заповнення додаткового масиву елементами
        sup_arr[arr[i] - min]++;

    for (int i = 1; i < sup_arr_size; i++) // цикл знаходження часткових сум
        sup_arr[i] = sup_arr[i] + sup_arr[i - 1];

    for (int i = 0; i < n; i++) {

        sort_sup_arr[sup_arr[arr[i] - min] - 1] = arr[i]; // кожен елемент arr[i] поміщається в масив sort_sup_arr яка дорівнює значенню елемента sup_arr[arr[i]]

        sup_arr[arr[i] - min]--;

    }
    for (int i = 0; i < n; i++) // повернення значення масивів
        arr[i] = sort_sup_arr[i];

    free(sort_sup_arr); // звільнення пам'яті масиву відсортованої послідовності
    free(sup_arr); // звільнення пам'яті додаткового масиву
}

void show_time(double time) {
    printf("Затрачений час сягає : %f с\n", time);
}

void input_from_arr(int* arr, int* saved_arr, int n) {
    for (int i = 0; i < n; i++)
        arr[i] = saved_arr[i];
}

void comparson(int n, int fill, int* arr, FILE* file, int* saved_arr) {
    for (int i = 0; i <= n; i += 1000) {
        printf("Поточна кількість %d елементів\n", i);
        for (int fill = 1; fill < 4; fill++)
            choice_sort(i, fill, arr, file, saved_arr);
        printf("\n");
    }
}

int choice_sort(int n, int fill, int* arr, FILE* file, int* saved_arr) // інтерфейсна частина програми(вибір методу сортування)
{
    time_t prev_clock, current_clock;
    double time; // тимчасові змінні
    switch (fill) // вибор типу сортування
    {
    case 1: // випадок сортування методом вибору й одночасне відслідковування пройденого часу
        prev_clock = clock();
        choice(n, arr);
        current_clock = clock();
        time = ((double)current_clock - (double)prev_clock) / CLOCKS_PER_SEC;
        file_print(arr, n, file);
        show_time(time);
        input_from_arr(arr, saved_arr, n);
        break;
    case 2: // випадок сортування методом Шелла й одночасне відслідковування пройденого часу
        prev_clock = clock();
        shell(n, arr);
        current_clock = clock();
        time = ((double)current_clock - (double)prev_clock) / CLOCKS_PER_SEC;
        file_print(arr, n, file);
        show_time(time);
        input_from_arr(arr, saved_arr, n);
        break;
    case 3: // випадок сортування методом підрахунків й одночасне відслідковування пройденого часу
        prev_clock = clock();
        count(n, arr);
        current_clock = clock();
        time = ((double)current_clock - (double)prev_clock) / CLOCKS_PER_SEC;
        file_print(arr, n, file);
        show_time(time);
        input_from_arr(arr, saved_arr, n);
        break;
    case 4:
        comparson(n, fill, arr, file, saved_arr);
        break;
    default: // випадок якщо не було обрано жодного з варіантів/помилка
        return -1;
    }
    return 0;
}

int random_fill_boarders(int i) {
    int low_limit, high_limit;
    if (i == 0) {
        printf("Введіть нижню та верхню границю функції rand() : ");

        if (!(scanf_s("%d", &low_limit)))
            return 1;

        return low_limit;
    }
    if (i == 1) {

        printf("\n");

        if (!(scanf_s("%d", &high_limit)))
            return -1;

        return high_limit;
    }
}

void saved_array(int* arr, int n, FILE* fp) {
    for (int i = 0; i < n; i++)
        fprintf(fp, "%d\n", arr[i]);
}

int fill_array(int n, int fill, int* arr, int* saved_arr, FILE* fp) // інтерфейсна частина програми(метод заповнення масиву)
{
    FILE* file;
    int flag, i = 0, low_limit = 0, high_limit = 0; // тимчасові змінні
    switch (fill) // вибір заповнення масиву
    {
    case 1: // випадок заповнення масиву випадковими числами
        low_limit = random_fill_boarders(0);
        high_limit = random_fill_boarders(1);
        if (high_limit < low_limit)
            return 5;
        int delta;
        delta = high_limit - low_limit + 1;
        for (int i = 0; i < n; ) {
            arr[i] = rand() % delta + low_limit;
            saved_arr[i] = arr[i];
            i++;
        }
        break;
    case 2: // випадок заповнення масиву вручну
        for (int i = 0; i < n; i++) {
            printf("Введіть %d елемент масиву : ", (i + 1));
            scanf_s("%d", &flag);
            arr[i] = flag;
        }
        break;
    case 3: // випадок заповнення масиву з файлу
        if ((file = fopen("ASD1.txt", "r")) == NULL) //Відкриття вхідного файлу з перевіркой
        {
            return 2;
        }
        while (feof(file) == 0)
        {
            if ((fscanf(file, "%d ", &arr[i])) != 1)
            {
                return 3;
            }
            i++;
        }
        if (fclose(file) == EOF)    //Перевірка на закриття файлу з закриттям
        {
            return 4;
        }
        break;
    default: // помилка
        return -1;
    }
    saved_array(arr, n, fp);
    return 0;
}

void file_print(int* arr, int n, FILE* file) {
    for (int i = 0; i < n; i++)
        fprintf(file, "%d\n", arr[i]);
}

int enter_size() // введення розміру масиву
{
    int n; // тимчасова змінна
    printf("Введіть розмір масиву: ");
    scanf_s("%d", &n);
    if (n < 1) // перевірка на правильність введення
        return 0;
    return n;
}

int errors(int flag, int n) // "Функція помилок"
{
    switch (n)
    {
    case 0:
        printf("Розмір масиву повинен бути більше 0 ! \n");
        return 0;
    default:
        break;
    }
    switch (flag)
    {
    case -1:
        printf("Неккоректно введено вибір ! \n");
        return 0;
    case 2:
        printf("He вдалося відкрити файл ! \n");
        return 0;
    case 3:
        printf("He вдалося зчитати файл ! \n");
        return 0;
    case 4:
        printf("He можу закрити файл ! \n");
        return 0;
    case 5:
        printf("Помилка при введені границь ! \n");
        return 0;
    default:
        return 1;
    }
}