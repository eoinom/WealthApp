﻿using backendAPI.Mutations;
using backendAPI.Queries;
using GraphQL;

namespace backendAPI.Schema
{
    public class backendSchema : GraphQL.Types.Schema
    {
        public backendSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<Query>();
            Mutation = resolver.Resolve<Mutation>();
        }
    }
}
