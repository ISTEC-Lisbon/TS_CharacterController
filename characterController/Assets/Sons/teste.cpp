# include <stdio.h>
# include <stdlib.h>
# include <time.h>

int main(){

	int i,max,min;
	srand((unsigned)time(NULL));
	int xpto[10]={1,3,4,5};
	//preencher
	int soma=0;
	for(int j=0; j<=9; j++){
		xpto[j]= rand()%50 +1;
		soma = soma + xpto[j];
	}
	max=xpto[0];
	min=xpto[0];
	for(int j=1; j<=9; j++){
		if(xpto[j]>max)max=xpto[j];
		if(xpto[j]<min)min=xpto[j];
	}
	//ordenacao
	int limite=9; //tamanho -1
	int troca;
	int pivot;
	do{
		troca =0;
		for(int i=0; i< limite; i++){
			if(xpto[i]>xpto[i+1]){
				pivot=xpto[i];
				xpto[i]=xpto[i+1];
				xpto[i+1]=pivot;
				troca=1;
			}
			
			
		}
		limite --;
	}while(troca !=0);
	
	//mostrar
	for( i=0; i<=9; i+=1)printf("%d-", xpto[i]);
	
	printf("\n");
	printf("\nsoma dos elementos do Array:%d",soma); 
		printf("\nMax: %d   Mín: %d",max,min); 
		
	system("pause");
}
