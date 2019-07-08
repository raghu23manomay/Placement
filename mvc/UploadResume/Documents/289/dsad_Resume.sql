--exec SP_LoadPurchase '2018-12-14'
alter proc SP_LoadPurchase 
(
@orderdate datetime =null
)
as
begin

DECLARE @cols AS varchar(MAX),
@query  AS NVARCHAR(MAX)

SELECT @cols = STUFF((SELECT distinct ',' + QUOTENAME(c.product) 
            FROM ProductMast c
            FOR XML PATH(''), TYPE
            ).value('.', 'varchar(MAX)') 
        ,1,1,'')
		    
SELECT @query =

'SELECT *
FROM    ( 
select distinct vm.VendorName,pm.Product,billamount as amount,p.quantity,cashamount as paidamount,
(select top 1 balancedue from SupplierPaymentDetail where supplierid = spd.supplierid) as previousbalance,
(select top 1 balancedue from SupplierPaymentDetail where supplierid = spd.supplierid) + (spd.billamount - spd.cashamount) as baldue,spd.billdate
from VendorMast vm 
left join Purchase p on p.VendorId = vm.VendorID
left join ProductMast pm on pm.productid = p.productid
inner join SupplierPaymentDetail spd on spd.supplierid = p.VendorId

) s
PIVOT (MAX(quantity) FOR Product IN ('+ @cols +')) pvt'

execute(@query)

end

--select * from ProductMast