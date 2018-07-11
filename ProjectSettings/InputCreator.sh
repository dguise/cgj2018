cp InputTemplateBase InputTemplateBase.temp
for i in `seq 1 16`;
do
    cp InputTemplate InputTemplate.temp
    sed -i -- "s/TEMP/$i/g" InputTemplate.temp
    cat InputTemplate.temp >> InputTemplateBase.temp
    rm InputTemplate.temp
done   
mv InputTemplateBase.temp InputManager.asset
