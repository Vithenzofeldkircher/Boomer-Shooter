using UnityEngine;
public abstract class Item : MonoBehaviour, ICollectable
{
    public abstract Element Collect();
    //METODOS ABSTRATOS
    //Força os filhos a implementarem
    //Usado quando todos os filhos usam, mas com comportamentos diferentes
    //Não declara o corpo, apenas a assinatura
    protected abstract void Teste1();

    //METODOS VIRTUAIS
    //Permite que os filhos sobrescrevam, mas não obriga
    //Quando apenas alguns dos filhos tem comportamento difetente
    protected virtual void Teste2()
    {
        //Corpo de metodo
    }
    //METODOS NORMAIS
    //Quando todos os filhos tem o mesmo comportamento
    protected void Teste3()
    {

    }
}