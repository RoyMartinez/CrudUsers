namespace CrudUsers.Shared;

public class NotFoundException(string message) : Exception(message)
{
}

public class FetchException(string message) : Exception(message)
{
}

public class CreateException(string message) : Exception(message)
{
}
public class UpdateException(string message) : Exception(message)
{
}

public class RemoveException(string message) : Exception(message)
{
}