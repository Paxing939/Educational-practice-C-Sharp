// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o Contacts.cs
using System;
using System.Collections;

public class Person
{
    public Person(string fName, string lName)
    {
        this.firstName = fName;
        this.lastName = lName;
    }

    public string firstName;
    public string lastName;
}

public class People : IEnumerable
{
    private Person[] _people;
    public People(Person[] pArray){
       _people = new Person[pArray.Length];
        for (int i = 0; i < pArray.Length; i++)
        {
            _people[i] = pArray[i];
        }
    }
    IEnumerator IEnumerable.GetEnumerator(){
        return new PeopleEnum(_people);
    }
}
public class PeopleEnum : IEnumerator
{
    public Person[] _people;
    int position = -1; 
    // positioned before the first element

    public PeopleEnum(Person[] list) {
        _people = list;
    }

    public bool MoveNext() {
        position++;
        return (position < _people.Length);
    }
    public void Reset() {
        position = -1;
    }
    public object Current {
        get { try {
                return _people[position];
            }
            catch (IndexOutOfRangeException) {
                throw new 
                    InvalidOperationException();
            }
        }
    }
}
class App {
    static void Main() {
        Person[] peopleArray = new Person[3] {
            new Person("John", "Smith"),
            new Person("Jim", "Johnson"),
            new Person("Sue", "Rabon"),
        };
        People peopleList = new People(peopleArray);
        foreach (Person p in peopleList)
            Console.WriteLine(p.firstName + 
                " " + p.lastName);
    }
}
