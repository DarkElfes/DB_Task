--Query 1
--Task: get Users that born before 2000 and sort by DateOfBirth in desc order
--Using: where, order by, desc
select *
from Users
where DateOfBirth < '2000.01.01'
order by DateOfBirth desc


--Query 2
--Task: get Users who have on card more than 5000 uah
--Using: join, where, or, and, order by, case - end, when - then
select u.FirstName, u.LastName, c.id as CardId, c.Number, c.Balance, c.Currency
from Users as u
         join Cards as c on u.Id = c.UserId
where c.Currency = 'uah' and c.Balance > 500
   or c.Currency = 'usd' and c.Balance > 500 / 42
   or C.Currency = 'eur' and c.Balance > 500 / 45
order by case
             when c.Currency = 'uah' then c.Balance
             when c.Currency = 'usd' then c.Balance * 42
             when c.Currency = 'eur' then c.Balance * 45
             end

--Query 3
--Task: get ATMs where city equals Odesa or Kyiv and balance more than 40000
--Using: join, where, or, and, order by, desc
select l.City, l.Address, a.Balance
from ATMs a
         join Locations l on a.LocationId = l.Id
where (l.City = 'Kyiv' or l.City = 'Odesa')
  and a.Balance > 40000
order by a.Balance desc

--Query 4
--Task: get last by time atm transactions for every city.
--Using: join, where, group by, having, order by, desc
select t.Amount,
       t.TimeStamp,
       t.Status,
       t.CardId,
       t.CardBalance,
       t.ATMTransactionType,
       l.City
from Transactions t
         join ATMs a on a.Id = t.ATMId
         join Locations l on a.LocationId = l.Id
where t.TimeStamp = (select max(t2.TimeStamp)
                     from Transactions t2
                              join ATMs a2 on a2.Id = t2.ATMId
                              join Locations l2 on a2.LocationId = l2.Id
                     where l2.City = l.City)
order by t.TimeStamp desc

--Query 5
--Task: get all cities where ATM is located
--Using: distinct
select distinct City
from Locations

--Query 6
--Task: get all transfer transaction for 2024.03.15 between 15 - 17 hours
--Using: where, between, and, order by, desc
select t.Amount, t.TimeStamp, t.Status, t.TransactionType
from transactions as t
where t.TransactionType = 'Transfer'
  and t.TimeStamp between '2024-03-15 15:00:00' and '2024-03-15 17:00:00'
order by t.TimeStamp desc

--Query 7
--Task: get all users whose email was created on domain 'student.ztu.edu.ua'
--Using: where, like, order by, desc
select *
from Users as u
where u.Email like '%student.ztu.edu.ua'
order by u.DateOfBirth desc

--Query 8
--Task: get average sum of withdrawing for city
--Using: join, where, group by, order by, desc
select l.City, avg(t.Amount) as [Average Withdrawing]
from Transactions t
         join ATMs as a on t.ATMId = a.Id
         join Locations as l on l.id = a.locationId
where t.ATMTransactionType = 'Withdraw'
group by l.City
order by [Average Withdrawing] desc

--Query 9
--Task: get all transactions where ATMId it's not null
--Using: where, is not null
select t.Amount, t.TimeStamp, t.TransactionType, t.ATMId, t.ATMTransactionType
from transactions as t
where t.ATMId is not null
-- or it's like: t.TransactionType = 'ATM'

--Query 10
--Task: get cities with more than 2 withdrawals
--Using: join, group by, having, order by
select l.City, count(t.Id) as TotalWithdrawals
from Transactions t
         join ATMs a ON t.ATMId = a.Id
         join Locations l ON l.Id = a.LocationId
where t.TransactionType = 'ATM'
  and t.ATMTransactionType = 'Withdraw'
group by l.City
having count(t.Id) > 2
ORDER BY TotalWithdrawals DESC;

--Query 11
--Task: get total balance of all users in UAH
--Using: sum, join, where
select sum(c.Balance) as TotalBalance
from Users u
         join Cards c on u.Id = c.UserId
where c.Currency = 'uah';

--Query 12
--Task: get the latest transaction for each user
--Using: join, group by, max
select u.FirstName, u.LastName, max(t.TimeStamp) as LatestTransaction
from Users u
         join Transactions t on u.Id = t.Id
group by u.FirstName, u.LastName;

--Query 13
--Task: get the number of transactions per ATM
--Using: join, group by, count
select a.Id as ATMId, count(t.Id) as TransactionCount
from ATMs a
         left join Transactions t on a.Id = t.ATMId
group by a.Id;

--Query 14
--Task: get the total number of users
--Using: count
select count(*) as TotalUsers
from Users;

--Query 15
--Task: get the maximum balance of cards by currency
--Using: max, join, group by
select c.Currency, max(c.Balance) as MaxBalance
from Cards c
group by c.Currency;





