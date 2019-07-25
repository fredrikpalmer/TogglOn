import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import FeatureToggles from './components/FeatureToggles';
import FeatureGroups from './components/FeatureGroups';

export default () => (
    <Layout>
        <Route exact path='/' component={FeatureToggles} />
        <Route path='/featuregroups' component={FeatureGroups} />
    </Layout>
);
