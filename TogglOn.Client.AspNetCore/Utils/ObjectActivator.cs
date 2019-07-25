﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TogglOn.Client.AspNetCore.Utils
{
    internal static class ObjectActivator
    {
        public static object Create<T>(object[] args)
        {
            var createDelegate = GetActivator(typeof(T));

            return createDelegate(args);
        }

        private static Func<object[], object> GetActivator(Type type)
        {

            var ctor = type.GetConstructors().First();
            ParameterInfo[] paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            ParameterExpression param =
                Expression.Parameter(typeof(object[]), "args");

            Expression[] argsExp =
                new Expression[paramsInfo.Length];

            //pick each arg from the params array 
            //and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp =
                    Expression.ArrayIndex(param, index);

                Expression paramCastExp =
                    Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            //make a NewExpression that calls the
            //ctor with the args we just created
            NewExpression newExp = Expression.New(ctor, argsExp);

            //create a lambda with the New
            //Expression as body and our param object[] as arg
            LambdaExpression lambda =
                Expression.Lambda(typeof(Func<object[], object>), newExp, param);

            //compile it
            return (Func<object[], object>)lambda.Compile();
        }
    }
}
