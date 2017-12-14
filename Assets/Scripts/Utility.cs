
public class Utility{

    //this function is used for creating the food sprites
    public static float convertValueIntoAlpha(int value){

        if(value == 1){

            return 0.2f;
        }

        if(value == 3){

            return 0.55f;
        }

        if(value == 5){

            return 1f;
        }

        return 0;
    }
}