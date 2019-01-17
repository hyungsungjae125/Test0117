DELIMITER \\
CREATE PROCEDURE p_Admin_Select(in _passwd varchar(10))
begin
	select aNo from Admin where passwd = _passwd;
end
\\

DELIMITER \\
CREATE PROCEDURE p_Category_Select()
begin
	select cNo,cName from Category;
end
\\

INSERT INTO Category (cName) VALUES ('에스프레소');
INSERT INTO Category (cName) VALUES ('음료');
INSERT INTO Category (cName) VALUES ('티');
INSERT INTO Category (cName) VALUES ('디저트');

DELIMITER \\
CREATE PROCEDURE p_Menu_Select(in _cNo int)
begin
	select mNo,mName,mPrice,mImage from Menu where cNo = _cNo and soldoutYn = 'N' and delYn='N';
end
\\

DELIMITER \\
CREATE PROCEDURE p_Menu_Choice(in _mName varchar(20))
begin
	select mNo,mName,mPrice,mImage,DegreeYn,SizeYn,ShotYn,CreamYn
			 from Menu where mName = _mName;	
end
\\

DELIMITER \\
CREATE PROCEDURE p_Orderlist_insert(
			in _mName varchar(20),in _oNum int, in _oCount int,in _oDegree varchar(10), in _oSize varchar(10),
			in _oShot int, in _oCream varchar(10)
			)
begin
	declare _mNo int;
	declare _allprice int;
	set _mNo = (select mNo from Menu where mName =_mName);
	set _allprice = (select mPrice from Menu where mName =_mName);
	if(_oShot != -1) 
	then set _allprice = _allprice + (_oShot * 500);
	end if;
	
	if(_oSize = 'Large') 
	then set _allprice = _allprice+500; 
	end if;
	
	insert into Orderlist (mNo,oNum,oCount,oDegree,oSize,oShot,oCream,allprice)values(_mNo,_oNum,_oCount,_oDegree,_oSize,_oShot,_oCream,_allprice);
end
\\

DELIMITER \\
CREATE PROCEDURE p_Orderlist_select()
begin
	
	select '',m.mName,o.oDegree,o.oSize,o.oShot,o.oCream,o.allprice,o.oCount,(o.oCount*o.allprice),o.oNo from Orderlist as o inner join Menu as m on (o.mNo=m.mNo) where orderYn='N';
end
\\

DELIMITER \\
CREATE PROCEDURE p_Orderlist_selectstaff()
begin
	select '',o.oNum,m.mName,o.oDegree,o.oSize,o.oShot,o.oCream,o.oCount,o.oNo from Orderlist as o inner join Menu as m on (o.mNo=m.mNo) where orderYn ='Y' and comYn = 'N' order by o.oNum;
end
\\

DELIMITER \\
CREATE PROCEDURE p_Menu_image(
			in _mName varchar(20)
			)
begin
	select mImage from Menu where mName =_mName;
end
\\

DELIMITER \\
CREATE PROCEDURE p_Orderlist_selectpay()
begin
	
	select '',m.mName,o.oDegree,o.oSize,o.oShot,o.oCream,o.allprice,o.oCount,(o.oCount*o.allprice) from Orderlist as o inner join Menu as m on (o.mNo=m.mNo) where orderYn='N';
end
\\

DELIMITER \\
CREATE PROCEDURE p_Orderlist_deleteOrder(in _oNo int)
begin
	delete from Orderlist where oNo = _oNo;
end
\\

DELIMITER \\
CREATE PROCEDURE p_Orderlist_deleteOrderAll(in _oNum int)
begin
	delete from Orderlist where oNum = _oNum;
end
\\

DELIMITER \\
CREATE PROCEDURE p_Orderlist_selectMaxoNum()
begin
	select case when max(oNum) is null then 0
            else max(oNum)
		  end from Orderlist  where orderYn = 'Y';
end
\\
	  
DELIMITER \\
CREATE PROCEDURE p_Orderlist_selectBill(in _oNum int)
begin
	select '',m.mName,o.oDegree,o.oSize,o.allprice,o.oCount,(o.allprice*o.oCount) from Orderlist as o inner join Menu as m on (o.mNo=m.mNo) where orderYn='Y' and oNum=_oNum;
end
\\

DELIMITER \\
CREATE PROCEDURE p_Admin_monthIncome(in _startdate varchar(15),in _enddate varchar(15))
begin
	select DATE_FORMAT(comDate, "%Y-%m") as '년/월',SUM(o.allprice*o.oCount) as '월별합계'  from Orderlist as o inner join Menu as m on (o.mNo=m.mNo)
	where comYn='Y' and DATE_FORMAT(comDate, "%Y-%m")>=_startdate and DATE_FORMAT(comDate, "%Y-%m") <= _enddate group by DATE_FORMAT(comDate, "%Y-%m");
end
\\

DELIMITER \\
CREATE PROCEDURE p_Admin_menuIncome(in _startdate varchar(15),in _enddate varchar(15))
begin
	select m.mName,SUM(o.oCount),SUM(o.allprice*o.oCount) from Orderlist as o left join Menu as m on (o.mNo=m.mNo)
	where comYn='Y' and DATE_FORMAT(comDate, "%Y-%m")>=_startdate and DATE_FORMAT(comDate, "%Y-%m") <= _enddate group by m.mName;
end
\\
	
	/*===============================================================================*/
delimiter \\
create PROCEDURE p_Menu_Insert(in _cNo int,in _mName VARCHAR(20),in _mPrice int,in _mImage VARCHAR(1000),in _DegreeYn int,in _SizeYn int,in _ShotYn int,in _CreamYn INT)
BEGIN
	declare _mNameselect varchar(20);
	set _mNameselect = (select mName from Menu where mName = _mName);
	if _mNameselect=_mName then update Menu SET cNo=_cNo,mPrice=_mPrice,mImage=_mImage,DegreeYn=_DegreeYn,SizeYn=_SizeYn,ShotYn=_SizeYn,CreamYn=_CreamYn,soldoutYn='N',delYn='N' WHERE mName=_mName;
	else INSERT INTO Menu (cNo,mName,mPrice,mImage,DegreeYn,SizeYn,ShotYn,CreamYn) VALUES (_cNo,_mName,_mPrice,_mImage,_DegreeYn,_SizeYn,_ShotYn,_CreamYn);
	end if;
end
\\
/*===============================================================================*/
DELIMITER \\
CREATE PROCEDURE p_Menu_NameSelect(in _cNo int)
begin
   SELECT '',mName from Menu where cNo = _cNo and delYn='N';
end
\\

call p_Menu_NameSelect(1);
/*===============================================================================*/
delimiter \\
create PROCEDURE p_Menu_Delete(in _mName VARCHAR(20))
BEGIN
	UPDATE Menu SET delYn='Y' WHERE mName=_mName;
end
\\

/*===============================================================================*/
delimiter \\
create PROCEDURE p_Menu_MenuEdeitSelect(in _mName VARCHAR(20))
BEGIN
	SELECT mName,mPrice,mImage,DegreeYn,SizeYn,ShotYn,CreamYn FROM Menu WHERE mName=_mName;
end
\\

/*===============================================================================*/
delimiter \\
create PROCEDURE p_Menu_MenuEdit(in _mName VARCHAR(20),in _NewmName VARCHAR(20),in _mPrice int,in _mImage VARCHAR(1000),in _DegreeYn int,in _SizeYn int,in _ShotYn int,in _CreamYn INT)
BEGIN
	UPDATE Menu SET mName=_NewmName,mPrice=_mPrice,mImage=_mImage,DegreeYn=_DegreeYn,SizeYn=_SizeYn,ShotYn=_ShotYn,CreamYn=_CreamYn  WHERE mName=_mName;
end
\\



/*===============================================================================*/
DELIMITER \\
CREATE PROCEDURE p_SoldoutList()
begin
   select '',cName,mName from Menu inner join Category on Menu.cNo = Category.cNo where Menu.soldoutYn='Y' AND Menu.delYn='N';
end
\\

call p_SoldoutList;

/*===============================================================================*/

delimiter \\
create PROCEDURE p_Staff_ComYn(in _oNo int)
BEGIN
	UPDATE Orderlist SET comYn='Y' WHERE oNo=_oNo;
end
\\
/*===============================================================================*/
delimiter \\
create PROCEDURE p_Staff_SoldOutAdd(in _mName VARCHAR(20))
BEGIN
	UPDATE Menu SET soldoutYn='Y' WHERE mName=_mName;
end
\\

call p_Staff_SoldOutAdd('티라미수 라떼');
/*===============================================================================*/
delimiter \\
create PROCEDURE p_Staff_SoldOutDelete(in _mName VARCHAR(20))
BEGIN
	UPDATE Menu SET soldoutYn='N' WHERE mName=_mName;
end
\\

call p_Staff_SoldOutDelete('티라미수 라떼');
/*===============================================================================*/
delimiter \\
create PROCEDURE p_Staff_SoldOutAddList(in _cNo int)
BEGIN
	select '',mName from Menu where cNo=_cNo and delYn='N'and soldoutYn='N';
end
\\


/*===============================================================================*/
delimiter \\
create PROCEDURE p_Staff_SoldOutDeleteList()
BEGIN
	select '',mName from Menu where delYn='N'and soldoutYn='Y';
end
\\

/*===============================================================================*/

delimiter \\
create PROCEDURE p_Orderlist_orderYn(IN _oNum int)
BEGIN
	UPDATE Orderlist SET orderYn='Y' WHERE _oNum=oNum;
end
\\

/*===============================================================================*/
delimiter \\
create PROCEDURE p_Admin_Passwd()
BEGIN
	SELECT passwd FROM Admin;
end
\\
