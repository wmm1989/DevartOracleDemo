
1: ִ�� init.sql ,���������ݲ��뵽700000������
2: �޸�appsetting �����ļ�������oracle �����ַ����� ����dbType�ֶ� 2�� ԭ��oracle dll  3��devart .
3: ���� JQ.Base.api  .   api/base/prcaddressdict  ����SuperiorAreaNumber=0 �� ����鿴

dbtype=2   ����

SELECT "b".ID, "b".AREALV, "b".AREANAME, "b".AREANUMBER, "b".AREASTANDARDCODE, "b".AREASTANDARDNAME, "b".CREATE_BY, "b".CREATE_NAME, "b".CREATE_TIME, "b".ISDEL, "b".ISUPLOAD, "b".ORG_CODE, "b".PYM, "b".SUPERIORAREANUMBER, "b".UPDATE_BY, "b".UPDATE_NAME, "b".UPDATE_ORG_CODE, "b".UPDATE_TIME, "b".UPLOAD_TIME, "b".WBM
FROM BASE_PRCADDRESS_DICT "b"
WHERE ("b".ISDEL = 0) AND ("b".SUPERIORAREANUMBER='0')

--AND ("b".SUPERIORAREANUMBER='0')

dbtype=3 ����

SELECT "b".ID, "b".AREALV, "b".AREANAME, "b".AREANUMBER, "b".AREASTANDARDCODE, "b".AREASTANDARDNAME, "b".CREATE_BY, "b".CREATE_NAME, "b".CREATE_TIME, "b".ISDEL, "b".ISUPLOAD, "b".ORG_CODE, "b".PYM, "b".SUPERIORAREANUMBER, "b".UPDATE_BY, "b".UPDATE_NAME, "b".UPDATE_ORG_CODE, "b".UPDATE_TIME, "b".UPLOAD_TIME, "b".WBM
FROM BASE_PRCADDRESS_DICT "b"
WHERE ("b".ISDEL = 0) AND ((DBMS_LOB.COMPARE("b".SUPERIORAREANUMBER, '0')) = 0)

-- AND ((DBMS_LOB.COMPARE("b".SUPERIORAREANUMBER, '0')) = 0)