using CustomEntityFoundation.Fields;
using EntityFrameworkCore.BootKit;

namespace CustomEntityFoundation.Nodes
{
    public class NodeTextField : TextField, IDbRecord { }

    public class NodeEntityReferenceField : EntityReferenceField, IDbRecord { }

    public class NodeDateTimeField : DateTimeField, IDbRecord { }

    public class NodeFileField : FileField, IDbRecord { }

    public class NodeIntegerField : IntegerField, IDbRecord { }

    public class NodeBooleanField : BooleanField, IDbRecord { }
}
