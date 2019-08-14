import React from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import NavMenu from './NavMenu';
import Spinner from './Spinner';
import Error from './Error';

export default props => (
  <Grid fluid>
    <Spinner />
    <Row>
      <Col sm={3}>
        <NavMenu />
      </Col>
      <Col sm={9}>
        <Error />

        {props.children}
      </Col>
    </Row>

  </Grid>
);
