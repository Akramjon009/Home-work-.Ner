-- clastered non clastered
Jismoniy tashkilot:

Jadvaldagi ma'lumotlar klasterlangan indeks qiymatlariga ko'ra jismoniy tartiblangan.
Jadval va klasterli indeks yagona jismoniy ma'lumotlar strukturasini ifodalaydi.
Ishlash:

Ma'lumotlarni olish uchun so'rovlar odatda tezroq bo'ladi, chunki ma'lumotlar allaqachon indeks tartibi bo'yicha tartiblangan.
Klasterli indeks bilan diapazon so'rovlari va saralash ko'pincha samaraliroq bo'ladi.
Cheklovlar:

Jadvalda faqat bitta klasterli indeks bo'lishi mumkin, chunki ma'lumotlar faqat bitta tartibda jismonan tartibga solinishi mumkin.
Kamchiliklari:

Yangi ma'lumotlarni kiritish yoki mavjud ma'lumotlarni yangilash qatorlarni ko'chirishni talab qilishi mumkin, bu esa qo'shimcha manba xarajatlarini talab qilishi mumkin.
Klasterli bo'lmagan indeks:
Mantiqiy ajratish:

Jadvaldagi ma'lumotlar klasterli bo'lmagan indeks tartibida jismonan tartiblanmagan. Indeksda ma'lumotlarning haqiqiy joylashuviga ishora qiluvchi tuzilma mavjud.
Indeks tuzilishi haqiqiy ma'lumotlardan alohida saqlanadi.
Ishlash:

Klasterli bo'lmagan indeks orqali ma'lumotlarni qidirish qo'shimcha operatsiyalarni talab qilishi mumkin, ammo bu indeks tartibiga mos kelmaydigan so'rovlar uchun samarali bo'lishi mumkin.
Bir nechta indekslar:

Jadvalda bir nechta klasterli bo'lmagan indekslar bo'lishi mumkin, bu unga har xil turdagi so'rovlarni qo'llab-quvvatlash imkonini beradi.
Yangilanishlar va qo'shimchalar:

Yangi ma'lumotlarni kiritish yoki mavjud ma'lumotlarni yangilash ko'pincha arzonroq, chunki qatorlarni ko'chirishga hojat yo'q.
Foydalanish misoli:

Klasterli bo'lmagan indeks ma'lum bir ustunda qidirishni tezlashtirishni xohlasangiz foydali bo'ladi, lekin siz ma'lumotlarning jismoniy tartibini o'zgartirishni xohlamaysiz.



--1
SELECT ID,
    CASE
		WHEN P_ID IS NULL THEN 'Root'
			WHEN ID IN(SELECT P_ID FROM TREE) THEN 'Inner'
			ELSE 'Leaf'
	END AS TYPE
FROM TREE;

--2
UPDATE SALARY
SET SEX = CASE
	WHEN SEX = 'm' THEN 'f'
	ELSE 'm'
END;
--3
SELECT
    Department.id,
    sum(CASE WHEN month = 'Jan' THEN revenue ELSE null END) AS Jan_Revenue,
    sum(CASE WHEN month = 'Feb' THEN revenue ELSE null END) AS Feb_Revenue,
    sum(CASE WHEN month = 'Mar' THEN revenue ELSE null END) AS Mar_Revenue,
    sum(CASE WHEN month = 'Apr' THEN revenue ELSE null END) AS Apr_Revenue,
    sum(CASE WHEN month = 'May' THEN revenue ELSE null END) AS May_Revenue,
    sum(CASE WHEN month = 'Jun' THEN revenue ELSE null END) AS Jun_Revenue,
    sum(CASE WHEN month = 'Jul' THEN revenue ELSE null END) AS Jul_Revenue,
    sum(CASE WHEN month = 'Aug' THEN revenue ELSE null END) AS Aug_Revenue,
    sum(CASE WHEN month = 'Sep' THEN revenue ELSE null END) AS Sep_Revenue,
    sum(CASE WHEN month = 'Oct' THEN revenue ELSE null END) AS Oct_Revenue,
    sum(CASE WHEN month = 'Nov' THEN revenue ELSE null END) AS Nov_Revenue,
    sum(CASE WHEN month = 'Dec' THEN revenue ELSE null END) AS Dec_Revenue
FROM
    Department
GROUP BY
    id;
--4
SELECT
    employee_id,
    CASE WHEN employee_id % 2 != 0 AND name NOT LIKE 'M%' THEN salary ELSE 0 END AS bonus
FROM
    Employees
ORDER BY
    employee_id;