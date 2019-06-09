package mjaganm.LearnDistributedSystems.BasicWebserver;

public class IsAlive
{

    private final long id;
    private final String content;

    public IsAlive(long id, String content) {
        this.id = id;
        this.content = content;
    }

    public long getId() {
        return id;
    }

    public String getContent() {
        return content;
    }
}
