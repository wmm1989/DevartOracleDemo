
CREATE TABLE Base_PRCAddress_Dict(
    ID INT NOT NULL, 
    Org_Code VARCHAR2(32), 
    AreaStandardCode VARCHAR2(32), 
    AreaStandardName VARCHAR2(32), 
    AreaNumber VARCHAR2(32), 
    AreaName VARCHAR2(128), 
    SuperiorAreaNumber VARCHAR2(32), 
    AreaLv VARCHAR2(32),
    PYM VARCHAR2(32), 
    WBM VARCHAR2(32), 
    Update_By VARCHAR2(32), 
    Update_Name VARCHAR2(32), 
    Update_Org_Code VARCHAR2(32), 
    Update_Time DATE, 
    IsDel INT NOT NULL, 
    Create_By VARCHAR2(32), 
    Create_Name VARCHAR2(32), 
    Create_Time DATE, 
    IsUpload INT NOT NULL, 
    Upload_Time DATE,
    PRIMARY KEY (ID)
);

create sequence BASEPRCADDRESSDICT_SEQ minvalue 1 maxvalue 9999999999999999999999999999 start with 1 increment by 1 cache 20;

create or replace trigger BASEPRCADDRESSDICT_TRG before insert on Base_PRCAddress_Dict for each row when (new.id is null or new.ID = 0)
begin
    select BASEPRCADDRESSDICT_SEQ.nextval into:new.id from dual;
end;


--插入SuperiorAreaNumber=0 的数据
DECLARE maxnumber CONSTANT int:=5;
  i int:=1;
  begin
    for i in 1..maxnumber loop
     insert into JQHIS0414.Base_PRCAddress_Dict(Org_Code,AreaStandardName,AreaNumber,SuperiorAreaNumber,AreaLv,IsDel,IsUpload)
VALUES('1000','1','1','0','1','0','0');
      end loop;
      DBMS_OUTPUT.PUT_LINE('成功录入数据');
      commit;
      end;

-- 插入700000条数据， SuperiorAreaNumber!=0 的数据，然后api查询SuperiorAreaNumber=0的数据
	  DECLARE maxnumber CONSTANT int:=700000;
  i int:=1;
  begin
    for i in 1..maxnumber loop
     insert into JQHIS0414.Base_PRCAddress_Dict(Org_Code,AreaStandardName,AreaNumber,SuperiorAreaNumber,AreaLv,IsDel,IsUpload)
VALUES('1000','1','1','1','1','0','0');
      end loop;
      DBMS_OUTPUT.PUT_LINE('成功录入数据');
      commit;
      end;