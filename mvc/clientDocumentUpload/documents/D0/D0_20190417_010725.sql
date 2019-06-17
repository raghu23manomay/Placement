select * from SupplierPaymentDetail where supplierid = 1
select * from Purchase where vendorid = 1
select * from CratesVendor where vendorid = 1

select distinct spd.supplierid as supplierid,billamount,spd.previousbalance,p.paidamount,spd.balancedue as balance,cv.cratesin,cv.cratesout 

from SupplierPaymentDetail spd inner join CratesVendor cv on
spd.supplierid = cv.vendorid
inner join Purchase p on p.billid = cv.billid


select distinct supplierid, (select top 1 billamount from SupplierPaymentDetail where supplierid = spd.supplierid and billamount!= 0.00 order by SupplierPaymentDetailID desc) as BILLAMOUNT,
(select top 1 balancedue from SupplierPaymentDetail where supplierid = spd.supplierid and billamount!= 0.00 order by SupplierPaymentDetailID desc) as BALANCEDUE,
(select top 1 previousbalance from SupplierPaymentDetail where supplierid = spd.supplierid and billamount!= 0.00 order by SupplierPaymentDetailID desc)  as previousbalance,
cv.cratesin,cv.cratesout,p.paidamount
from SupplierPaymentDetail spd
inner join CratesVendor cv on
spd.supplierid = cv.vendorid
left join Purchase p on
spd.supplierid = p.vendorid




select * from AreaMast