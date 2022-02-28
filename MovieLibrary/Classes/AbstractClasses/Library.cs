namespace MovieLibrary.Classes;

public abstract class Library<TE>
{
    private List<TE> mediaList = new List<TE>();

    public abstract void addMedia(TE media);

    public abstract String listMedia();
}