
1: 执行 init.sql ,建表，数据插入到700000条左右
2: 修改appsetting 配置文件，添加oracle 链接字符串。 其中dbType字段 2是 原先oracle dll  3是devart .
3: 运行 JQ.Base.api  .   api/base/prcaddressdict  传入SuperiorAreaNumber=0 。 输出查看

dbtype=2   如下

SELECT "b".ID, "b".AREALV, "b".AREANAME, "b".AREANUMBER, "b".AREASTANDARDCODE, "b".AREASTANDARDNAME, "b".CREATE_BY, "b".CREATE_NAME, "b".CREATE_TIME, "b".ISDEL, "b".ISUPLOAD, "b".ORG_CODE, "b".PYM, "b".SUPERIORAREANUMBER, "b".UPDATE_BY, "b".UPDATE_NAME, "b".UPDATE_ORG_CODE, "b".UPDATE_TIME, "b".UPLOAD_TIME, "b".WBM
FROM BASE_PRCADDRESS_DICT "b"
WHERE ("b".ISDEL = 0) AND ("b".SUPERIORAREANUMBER='0')

--AND ("b".SUPERIORAREANUMBER='0')

dbtype=3 如下

SELECT "b".ID, "b".AREALV, "b".AREANAME, "b".AREANUMBER, "b".AREASTANDARDCODE, "b".AREASTANDARDNAME, "b".CREATE_BY, "b".CREATE_NAME, "b".CREATE_TIME, "b".ISDEL, "b".ISUPLOAD, "b".ORG_CODE, "b".PYM, "b".SUPERIORAREANUMBER, "b".UPDATE_BY, "b".UPDATE_NAME, "b".UPDATE_ORG_CODE, "b".UPDATE_TIME, "b".UPLOAD_TIME, "b".WBM
FROM BASE_PRCADDRESS_DICT "b"
WHERE ("b".ISDEL = 0) AND ((DBMS_LOB.COMPARE("b".SUPERIORAREANUMBER, '0')) = 0)

-- AND ((DBMS_LOB.COMPARE("b".SUPERIORAREANUMBER, '0')) = 0)  这句话执行非常慢


同样的api，原生oracle dll 只需要20ms.而devart oracle 需要2000-20000ms

----------------------------------------------------------------------------------------------------
1: Execute init.sql, create tables, and insert data into about 700000 pieces

2: Modify the appsetting configuration file and add the Oracle link string. Where dbtype field 2 is the original Oracle DLL 3 is a device

3: Run JQ. Base. API. API / base / prcadddressdict to pass in superior area number = 0. Output view



Dbtype = 2 as follows



SELECT "b".ID, "b".AREALV, "b".AREANAME, "b".AREANUMBER, "b".AREASTANDARDCODE, "b".AREASTANDARDNAME, "b".CREATE_BY, "b".CREATE_NAME, "b".CREATE_TIME, "b".ISDEL, "b".ISUPLOAD, "b".ORG_CODE, "b".PYM, "b".SUPERIORAREANUMBER, "b".UPDATE_BY, "b".UPDATE_NAME, "b".UPDATE_ORG_CODE, "b".UPDATE_TIME, "b".UPLOAD_TIME, "b".WBM

FROM BASE_PRCADDRESS_DICT "b"

WHERE ("b".ISDEL = 0) AND ("b".SUPERIORAREANUMBER='0')



--AND ("b".SUPERIORAREANUMBER='0')



Dbtype = 3 as follows



SELECT "b".ID, "b".AREALV, "b".AREANAME, "b".AREANUMBER, "b".AREASTANDARDCODE, "b".AREASTANDARDNAME, "b".CREATE_BY, "b".CREATE_NAME, "b".CREATE_TIME, "b".ISDEL, "b".ISUPLOAD, "b".ORG_CODE, "b".PYM, "b".SUPERIORAREANUMBER, "b".UPDATE_BY, "b".UPDATE_NAME, "b".UPDATE_ORG_CODE, "b".UPDATE_TIME, "b".UPLOAD_TIME, "b".WBM

FROM BASE_PRCADDRESS_DICT "b"

WHERE ("b".ISDEL = 0) AND ((DBMS_LOB.COMPARE("b".SUPERIORAREANUMBER, '0')) = 0)



--And ((dbms_lob. Compare ("B". Superioreanumber, '0')) = 0) this sentence is very slow to execute




For the same API, the native Oracle DLL only needs 20ms, while the device Oracle needs 2000-20000ms
