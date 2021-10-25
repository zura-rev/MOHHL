import React, { useContext } from 'react'
import { BrowserRouter as Router, Switch, Route, Redirect } from 'react-router-dom'
import { routes } from './routes'
import { AuthPage } from './pages/AuthPage'
import { Layout } from './layout'
import { AuthContext } from './context/AuthProvider'

function PrivateRoute({ user, ...route }) {
  if (route.permissons.includes(user.resources)) {
    return (
      <Route
        path={route.path}
        {...route.exact}
        render={props => (
          <route.component {...props} />
        )}
      />
    )
  }
}

const App = () => {
  const { user } = useContext(AuthContext)
  return (
    <Router>
      {
        (user) ?
          (<Layout>
            <Switch>
              {
                routes.map(route => <PrivateRoute key={route.id} user={user} {...route} />)
              }
              <Redirect to='/calls' />
            </Switch>
          </Layout>) : (<>
            <Route path='/login' exact>
              <AuthPage />
            </Route>
            <Redirect to='/login' />
          </>)
      }
    </Router>
  )
}

export default App
