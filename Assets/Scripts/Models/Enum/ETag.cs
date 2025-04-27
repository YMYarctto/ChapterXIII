namespace ETag
{
    public enum Efficacy
    {
        安眠,
        提神,
        止泻,
        清热,
        止咳,
        解毒,
        止血,
        止痛,
        安神,
        止吐,
        抗过敏,
        抗菌,
        offset欣快,
        offset致幻,
    }

    // offset开头的为补缺没有副作用的效果
    public enum SideEffect
    {
        失眠,
        嗜睡,
        腹泻,
        上火,
        咳嗽,
        中毒,
        出血,
        疼痛,
        焦虑,
        恶心,
        过敏,
        offset抗菌,
        欣快,
        致幻,
    }
}
